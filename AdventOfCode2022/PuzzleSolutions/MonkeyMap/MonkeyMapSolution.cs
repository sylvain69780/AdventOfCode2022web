using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode2022Solutions.PuzzleSolutions;

namespace AdventOfCode2022Solutions.PuzzleSolutions.MonkeyMap
{
    public class MonkeyMapSolution : IPuzzleSolutionIter
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

        private string _puzzleInput = string.Empty;

        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }

        public IEnumerable<string> SolveFirstPart()
        {
            var simulation = ProcessInput(_puzzleInput);
            foreach (var (move, rotation) in simulation.Instructions)
            {
                for (var step = 0; step < move; step++)
                {
                    var tmp = ComputeNextPosition(simulation);
                    if (Map(simulation, tmp) != '#')
                        simulation.Position = tmp;
                    simulation.Step++;
                }
                if (rotation == "R")
                    simulation.Direction = (Direction)(((int)simulation.Direction + 1) % 4);
                if (rotation == "L")
                    simulation.Direction = (Direction)(((int)simulation.Direction - 1 + 4) % 4);
            }
            yield return (1000 * (simulation.Position.Y + 1) + 4 * (simulation.Position.X + 1) + (int)simulation.Direction).ToString();
        }

        public IEnumerable<string> SolveSecondPart()
        {
            var simulation = ProcessInput(_puzzleInput);
            foreach (var (move, rotation) in simulation.Instructions)
            {
                for (var step = 0; step < move; step++)
                {
                    var tmp = ComputeNextPositionOnCube(simulation);
                    if (Map(simulation, tmp.Position) != '#')
                        (simulation.Position, simulation.Direction) = tmp;
                    simulation.Step++;
                }
                if (rotation == "R")
                    simulation.Direction = (Direction)(((int)simulation.Direction + 1) % 4);
                if (rotation == "L")
                    simulation.Direction = (Direction)(((int)simulation.Direction - 1 + 4) % 4);
            }
            yield return (1000 * (simulation.Position.Y + 1) + 4 * (simulation.Position.X + 1) + (int)simulation.Direction).ToString();
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
        /// Le problème posé par ce puzzle est simple en apparence. On dispose en entrée du patron d'un cube avec des obstacles dessinés
        /// dessus. On doit déterminer la position finale d'un curseur qui se déplace sur cette carte en étant éventuellement bloqué par
        /// les obstacles. La première partie du puzzle est assez triviale puisqu'il faut se déplacer sur la carte en deux dimensions.
        /// La seconde partie demande de faire la même chose mais en représentant le cube issu du pliage de ce patron. Il y a plusieurs
        /// façons de dessiner le patron d'un cube. Faire une forme en croix avec 6 carrés accolés est la façon la plus courante. Mais
        /// ici, les formes données sont plus biscornues et compliquées. Pour pouvoir résoudre le puzzle, il faut se représenter chaque
        /// face du cube et déterminer les faces voisines ainsi que leur emplacement sur le patron. C'est une première difficulté, la
        /// seconde étant que l'orientation des carrés entre eux change lorsque l'on enroule le patron pour en faire un cube. Il me
        /// semble que c'est un problème finalement assez diabolique. La fonction ci-dessus est le fruit de nombreuses heures d'essais
        /// et d'erreurs. Une chose à réaliser est qu'un déplacement d'un carré sur le patron correspond à une rotation du cube d'un
        /// quart de tour. Une première astuce est de simplifier le problème en recherchant la position sur la carte de la face à
        /// droite de la face courante. L'algorithme de recherche des autres faces voisines (droite, haute, basse) est obtenu en
        /// changeant par rotation la direction de la recherche sur le patron. Pour trouver ce carré du patron qui est le voisin de
        /// droite sur le cube, on fait tourner un algorithme de type recherche en largeur, en se déplaçant de carré voisin en carré
        /// voisin sur le patron tout en déterminant à chaque fois la nouvelle position du cube dans l'espace. Cette position peut être
        /// représentée par une matrice de rotation 3D comme je l'ai vu proposé sur le fil Reddit
        /// https://www.reddit.com/r/adventofcode/comments/zsct8w/2022_day_22_solutions/. J'ai ici fait une simplification en
        /// utilisant simplement un vecteur à 3 dimensions, chaque dimension étant occupée par une valeur spécifique 1, 2 ou 3
        /// permettant d'identifier les 3 faces "right", "up" et "front" car dans un cube elles sont orthogonales entre elles. Lorsque
        /// la valeur 1, qui correspond initialement à la face de droite, se retrouve à la place de la valeur 3, on sait qu'on a atteint
        /// la position souhaitée. L'orientation de la face est alors déterminée par la position et le signe de la valeur 2.
        /// 
        /// The problem posed by this puzzle is deceptively simple. We are given a pattern of a cube with obstacles drawn on it as input.
        /// The task is to determine the final position of a cursor that moves on this map, potentially being blocked by the obstacles.
        /// The first part of the puzzle is relatively trivial as it involves moving on the map in two dimensions.
        /// The second part requires doing the same but representing the cube resulting from folding this pattern.
        /// There are several ways to draw the pattern of a cube, with the most common being a cross shape formed by 6 adjacent squares.
        /// However, here the given shapes are more irregular and complicated. To solve the puzzle, one needs to visualize each face of
        /// the cube, determine their neighboring faces, and their positions on the pattern. This is the first challenge, with the second
        /// being that the orientation of the squares changes when we wrap the pattern to form a cube.
        /// It seems to me that it is ultimately quite a devilish problem.
        /// The above function is the result of numerous hours of trial and error. One thing to realize is that moving 1 square on the
        /// pattern corresponds to a quarter-turn rotation of the cube.
        /// One initial trick is to simplify the problem by searching for the position on the map of the face to the right of the current face.
        /// The algorithm to find the other neighboring faces (right, up, down) is obtained by rotating the search direction on the pattern.
        /// To find the square on the pattern that is the right neighbor on the cube, we rotate a breadth-first search algorithm,
        /// moving from one neighboring square to another on the pattern while determining the new position of the cube in space each time.
        /// This position can be represented by a 3D rotation matrix as I saw proposed on the Reddit thread
        /// https://www.reddit.com/r/adventofcode/comments/zsct8w/2022_day_22_solutions/
        /// Here, I have simplified it by using a simple 3-dimensional vector, with each dimension occupied by a specific value, 1, 2, or 3,
        /// to identify the 3 faces "right," "up," and "front" since in a cube, they are orthogonal to each other.
        /// When the value 1, which initially corresponds to the right face, ends up in the position of value 3, we know that we have reached
        /// the desired position. The orientation of the face is then determined by the position and sign of value 2.        
        /// </summary>
        /// <param name="simulation"></param>
        /// <returns>Target boxId and rotation</returns>
        private static ((int X, int Y) boxId, int rotation) TargetBoxAndRotation(Simulation simulation)
        {
            var explored = new HashSet<(int X, int Y)>();
            var bfs = new Queue<((int X, int Y) BoxId, (int x, int y, int z) Cube)>();
            var boxId = (X: simulation.Position.X / simulation.Side, Y: simulation.Position.Y / simulation.Side);
            bfs.Enqueue((boxId, (1, 2, 3)));
            while (bfs.TryDequeue(out var item))
            {
                if (Map(simulation, (item.BoxId.X * simulation.Side, item.BoxId.Y * simulation.Side)) == ' ' || explored.Contains(item.BoxId))
                    continue;
                if (item.Cube.z == 1)
                    return (item.BoxId, item.Cube.y == 2 ? 0 : item.Cube.y == -2 ? 2 : item.Cube.x == 2 ? 1 : 3);
                else
                {
                    explored.Add(item.BoxId);
                    var (dirX, dirY) = Directions2D[(int)simulation.Direction];
                    bfs.Enqueue(((item.BoxId.X + dirX, item.BoxId.Y + dirY), RotCubeLeft(item.Cube)));
                    bfs.Enqueue(((item.BoxId.X - dirX, item.BoxId.Y - dirY), RotCubeRight(item.Cube)));
                    bfs.Enqueue(((item.BoxId.X + dirY, item.BoxId.Y - dirX), RotCubeDown(item.Cube)));
                    bfs.Enqueue(((item.BoxId.X - dirY, item.BoxId.Y + dirX), RotCubeUp(item.Cube)));
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
            for (var row = 0; row < simulation.Map.Length; row++)
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
            return sb.ToString();
        }
    }
}