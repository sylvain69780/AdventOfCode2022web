using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(22, "Monkey Map")]
    public class MonkeyMap : IPuzzleSolverV2
    {
        private struct Simulation
        {
            public string[] Map;
            public List<(int Move, string Rotation)> Instructions;
            public (int X, int Y) Position;
            public Direction Direction;
            public int MaxY => Map.Length - 1;
            public int MaxX => Map[Position.Y].Length - 1;
            public int Side;
            public int Step;
        }

        public async Task<string> SolveFirstPart(string inp, Func<Func<string>, Task> update, CancellationToken token)
        {
            var simulation = ProcessInput(inp);
            foreach (var (move, rotation) in simulation.Instructions)
            {
                for (var step = 0; step < move; step++)
                {
                    var tmp = ComputeNextPosition(simulation);
                    if (Map(simulation, tmp) != '#')
                        simulation.Position = tmp;
                    simulation.Step++;
                    await update(() => DisplayMap(simulation));
                }
                if (rotation == "R")
                    simulation.Direction = (Direction)(((int)simulation.Direction + 1) % 4);
                if (rotation == "L")
                    simulation.Direction = (Direction)(((int)simulation.Direction - 1 + 4) % 4);
            }
            return (1000 * (simulation.Position.Y + 1) + 4 * (simulation.Position.X + 1) + (int)simulation.Direction).ToString();
        }

        public async Task<string> SolveSecondPart(string inp, Func<Func<string>, Task> update, CancellationToken token)
        {
            var simulation = ProcessInput(inp);
            foreach (var (move, rotation) in simulation.Instructions)
            {
                for (var step = 0; step < move; step++)
                {
                    var tmp = ComputeNextPositionOnCube(simulation);
                    if (Map(simulation, tmp.Position) != '#')
                        (simulation.Position, simulation.Direction) = tmp;
                    simulation.Step++;
                    await update(() => DisplayMap(simulation));
                }
                if (rotation == "R")
                    simulation.Direction = (Direction)(((int)simulation.Direction + 1) % 4);
                if (rotation == "L")
                    simulation.Direction = (Direction)(((int)simulation.Direction - 1 + 4) % 4);
            }
            return (1000 * (simulation.Position.Y + 1) + 4 * (simulation.Position.X + 1) + (int)simulation.Direction).ToString();
        }

        private static (int X, int Y) ComputeNextPosition(Simulation simulation)
        {
            var pos = simulation.Position;
            do
            {
                pos = MoveInDirection(pos, simulation.Direction);
                if (pos.X > simulation.MaxX)
                    pos.X = 0;
                else if (pos.Y > simulation.MaxY)
                    pos.Y = 0;
                else if (pos.X < 0)
                    pos.X = simulation.MaxX;
                else if (pos.Y < 0)
                    pos.Y = simulation.MaxY;
            } while (Map(simulation, pos) == ' ');
            return pos;
        }
        private static ((int X, int Y) Position, Direction Direction) ComputeNextPositionOnCube(Simulation simulation)
        {
            var direction = simulation.Direction;
            var position = MoveInDirection(simulation.Position, direction);
            if (Map(simulation, position) == ' ')
            {
                var (boxId, rotation) = TargetBoxAndRotation(simulation);
                direction = (Direction)(((int)direction + rotation) % 4);
                var (x, y) = ((position.X + simulation.Side) % simulation.Side, (position.Y + simulation.Side) % simulation.Side);
                for (var i = 0; i < rotation; i++)
                    (x, y) = (simulation.Side - y - 1, x);
                position = (x + boxId.X * simulation.Side, y + boxId.Y * simulation.Side);
            }
            return (position, direction);
        }

        /// <summary>
        /// Represent the current box on the map as the front face of a 3D cube, normal vector (x:0,y:0,z:1)<br/>
        /// One thing to realize is that when we move to a next box on the map we are rotating this cube at 90 degres on ether X or Y axis.<br/>
        /// Select a target face. Using symetry we need only to compute the path to the right face, normal vector (x:1,y:0,z:0).<br/>
        /// Start a Broard First Search algorithm tracking the rotation of the right face of the cube and the orientation vector (x:0,y:1;z:0)
        /// as we are moving on the 2D map.<br/>
        /// Stop the BFS and return the boxId when the target face becomes the front face.<br/>
        /// Deduce the rotation from the change of the orientation vector during these rotations.
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns>Target boxId and rotation</returns>
        private static ((int X, int Y) boxId, int rotation) TargetBoxAndRotation(Simulation simulation)
        {
            var explored = new HashSet<(int X, int Y)>();
            var bfs = new Queue<((int X, int Y) BoxId, (int x, int y, int z) Face, (int x, int y, int z) Orientation)>();
            var boxId = (X: simulation.Position.X / simulation.Side, Y: simulation.Position.Y / simulation.Side);
            bfs.Enqueue((boxId, (1, 0, 0), (0, 1, 0)));
            while (bfs.TryDequeue(out var item))
            {
                if (Map(simulation, (item.BoxId.X * simulation.Side, item.BoxId.Y * simulation.Side)) == ' ' || explored.Contains(item.BoxId))
                    continue;
                if (item.Face == (0, 0, 1))
                    return (item.BoxId, item.Orientation.y == 1 ? 0 : item.Orientation.y == -1 ? 2 : item.Orientation.x == 1 ? 1 : 3);
                else
                {
                    explored.Add(item.BoxId);
                    var (dirX, dirY) = Directions2D[(int)simulation.Direction];
                    bfs.Enqueue(((item.BoxId.X + dirX, item.BoxId.Y + dirY), RotCubeLeft(item.Face), RotCubeLeft(item.Orientation)));
                    bfs.Enqueue(((item.BoxId.X - dirX, item.BoxId.Y - dirY), RotCubeRight(item.Face), RotCubeRight(item.Orientation)));
                    bfs.Enqueue(((item.BoxId.X + dirY, item.BoxId.Y - dirX), RotCubeDown(item.Face), RotCubeDown(item.Orientation)));
                    bfs.Enqueue(((item.BoxId.X - dirY, item.BoxId.Y + dirX), RotCubeUp(item.Face), RotCubeUp(item.Orientation)));
                }
            }
            throw new NotSupportedException("Target box not found on the map.");
        }

        private static Simulation ProcessInput(string inputString)
        {
            var input = inputString.Split("\n");
            var inputCommands = input[^1];
            var regex = new Regex(@"(\d+)([L|R|E])");
            var instructions = regex.Matches(inputCommands + "E").Select(x => (Move: int.Parse(x.Groups[1].Value), Rotate: x.Groups[2].Value)).ToList();
            Array.Resize(ref input, input.Length - 2);
            return new Simulation()
            {
                Map = input,
                Instructions = instructions,
                Position = (input[0].IndexOf('.'), 0),
                Direction = Direction.Right,
                Side = input.Length > 50 ? 50 : 4
            };
        }

        private enum Direction
        {
            Right = 0,
            Down = 1,
            Left = 2,
            Up = 3
        }

        private static char Map(Simulation puzzleInput, (int X, int Y) pos)
        {
            if (pos.X < 0 || pos.Y < 0 || pos.Y > puzzleInput.MaxY || pos.X > puzzleInput.Map[pos.Y].Length - 1)
                return ' ';
            else
                return puzzleInput.Map[pos.Y][pos.X];
        }

        private static readonly (int X, int Y)[] Directions2D = new (int X, int Y)[] { (1, 0), (0, 1), (-1, 0), (0, -1) };

        private static (int X, int Y) MoveInDirection((int X, int Y) pos, Direction orientationName)
        {
            var (x, y) = Directions2D[(int)orientationName];
            return (pos.X + x, pos.Y + y);
        }

        static (int X, int Y, int Z) RotCubeLeft((int X, int Y, int Z) p)
            => (-p.Z, p.Y, p.X);
        static (int X, int Y, int Z) RotCubeRight((int X, int Y, int Z) p)
            => (p.Z, p.Y, -p.X);
        static (int X, int Y, int Z) RotCubeDown((int X, int Y, int Z) p)
            => (p.X, -p.Z, p.Y);
        static (int X, int Y, int Z) RotCubeUp((int X, int Y, int Z) p)
            => (p.X, p.Z, -p.Y);

        private static string DisplayMap(Simulation simulation)
        {
            //if (board.CubeFaceSize == 50)
            //    return "too large";
            var sb = new StringBuilder();
            for (var row = 0; row<simulation.Map.Length;row++)
            {
                for (var col = 0; col < simulation.Map[row].Length; col++)
                {
                    if (simulation.Position == (col, row))
                        sb.Append(
                            simulation.Direction == Direction.Right ? '>' :
                            simulation.Direction == Direction.Left ? '<' :
                            simulation.Direction == Direction.Up ? '^' : 'v'
                            );
                    else
                        sb.Append(simulation.Map[row][col]);
                }
                sb.Append('\n');
            }
            sb.Append($"Step {simulation.Step}\n");
            return  sb.ToString();
        }
    }
}