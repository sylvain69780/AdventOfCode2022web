using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(22, "Monkey Map")]
    public class MonkeyMap : IPuzzleSolver
    {
        private struct Board
        {
            public string[] Map;
            public List<(int Move, string Rotation)> Instructions;
            public (int X, int Y) PositionOnMap;
            public OrientationName OrientationName;
            public int MaxY => Map.Length - 1;
            public int MaxX => Map[PositionOnMap.Y].Length - 1;
            public int CubeFaceSize;
        }

        private static Board ProcessInput(string puzzleInput)
        {
            var input = puzzleInput.Split("\n");
            var cubeFaceSize = input.Length > 50 ? 50 : 4;
            var inputCommands = input[^1];
            var regex = new Regex(@"(\d+)([L|R|E])");
            var instructions = regex.Matches(inputCommands + "E").Select(x => (Move: int.Parse(x.Groups[1].Value), Rotate: x.Groups[2].Value)).ToList();
            Array.Resize(ref input, input.Length - 2);
            return new Board()
            {
                Map = input,
                Instructions = instructions,
                PositionOnMap = (input[0].IndexOf('.'), 0),
                OrientationName = OrientationName.Right,
                CubeFaceSize = cubeFaceSize
            };
        }
        private enum OrientationName
        {
            Right = 0,
            Down = 1,
            Left = 2,
            Up = 3
        }

        private static char Map(Board board, (int X, int Y) pos)
        {
            if (pos.X < 0 || pos.Y < 0 || pos.Y > board.MaxY || pos.X > board.Map[pos.Y].Length - 1)
                return ' ';
            else
                return board.Map[pos.Y][pos.X];
        }

        private static readonly (int X, int Y)[] Directions2D = new (int X, int Y)[] { (1, 0), (0, 1), (-1, 0), (0, -1) };

        private static (int X, int Y) MoveInDirection((int X, int Y) pos, OrientationName orientationName)
        {
            var dir = Directions2D[(int)orientationName];
            pos = (pos.X + dir.X, pos.Y + dir.Y);
            return pos;
        }

        public string SolveFirstPart(string inp)
        {
            var board = ProcessInput(inp);
            foreach (var (move, rotation) in board.Instructions)
            {
                Debug.WriteLine($"{move} {rotation}");
                for (var step = 0; step < move; step++)
                {
                    var tmp = ComputeNextPositionOnDevelopedCube(board);
                    if (Map(board, tmp) == '.')
                        board.PositionOnMap = tmp;
                }
                if (rotation == "R")
                    board.OrientationName = (OrientationName)(((int)board.OrientationName + 1) % 4);
                if (rotation == "L")
                    board.OrientationName = (OrientationName)(((int)board.OrientationName - 1 + 4) % 4);
            }
            return (1000 * (board.PositionOnMap.Y + 1) + 4 * (board.PositionOnMap.X + 1) + (int)board.OrientationName).ToString();
        }

        public string SolveSecondPart(string inp)
        {
            var board = ProcessInput(inp);
            foreach (var (move, rotation) in board.Instructions)
            {
                Debug.WriteLine($"{move} {rotation}");
                for (var step = 0; step < move; step++)
                {
                    var tmp = ComputeNextPositionOnCube(board);
                    if (Map(board, tmp.Position) == '.')
                        (board.PositionOnMap, board.OrientationName) = tmp;
                    DisplayMap(board);
                }
                if (rotation == "R")
                    board.OrientationName = (OrientationName)(((int)board.OrientationName + 1) % 4);
                if (rotation == "L")
                    board.OrientationName = (OrientationName)(((int)board.OrientationName - 1 + 4) % 4);
            }
            return (1000 * (board.PositionOnMap.Y + 1) + 4 * (board.PositionOnMap.X + 1) + (int)board.OrientationName).ToString();
        }

        private static (int X, int Y) ComputeNextPositionOnDevelopedCube(Board board)
        {
            var pos = board.PositionOnMap;
            do
            {
                pos = MoveInDirection(pos, board.OrientationName);
                if (pos.X > board.MaxX)
                    pos.X = 0;
                else if (pos.Y > board.MaxY)
                    pos.Y = 0;
                else if (pos.X < 0)
                    pos.X = board.MaxX;
                else if (pos.Y < 0)
                    pos.Y = board.MaxY;
            } while (Map(board, pos) == ' ');
            return pos;
        }

        static (int X, int Y, int Z) Rot3DFace90XClockWise((int X, int Y, int Z) p)
            => (p.X, -p.Z, p.Y);
        static (int X, int Y, int Z) Rot3DFace90XAntiClockWise((int X, int Y, int Z) p)
            => (p.X, p.Z, -p.Y);
        static (int X, int Y, int Z) Rot3DFace90YClockWise((int X, int Y, int Z) p)
            => (-p.Z, p.Y, p.X);
        static (int X, int Y, int Z) Rot3DFace90YAntiClockWise((int X, int Y, int Z) p)
            => (p.Z, p.Y, -p.X);

        private static ((int X, int Y) Position, OrientationName D) ComputeNextPositionOnCube(Board board)
        {
            var pos2D = board.PositionOnMap;
            var orientationName = board.OrientationName;
            var faceId2D = (X: pos2D.X / board.CubeFaceSize, Y: pos2D.Y / board.CubeFaceSize);
            pos2D = MoveInDirection(pos2D, board.OrientationName);
            if (Map(board, pos2D) == ' ')
            {
                var dir2D = Directions2D[(int)orientationName];
                var cubeFaceFront = (0, 0, 1);
                var cubeFaceRight = (1, 0, 0);
                var cubeFaceBorder = (0, 1, 0);
                var explored = new HashSet<(int X, int Y)>();
                var bfs = new Queue<((int X, int Y) CellId2D, (int x, int y, int z) border, (int x, int y, int z) face)>();
                bfs.Enqueue((faceId2D, cubeFaceBorder, cubeFaceRight));
                while (bfs.TryDequeue(out var x))
                {
                    if (Map(board, (x.CellId2D.X * board.CubeFaceSize, x.CellId2D.Y * board.CubeFaceSize)) == ' ' || explored.Contains(x.CellId2D))
                        continue;
                    if (x.face == cubeFaceFront)
                    {
                        var posInFace = (X: (pos2D.X + board.CubeFaceSize) % board.CubeFaceSize, Y: (pos2D.Y + board.CubeFaceSize) % board.CubeFaceSize);
                        var rotation = (OrientationName)(
                            x.border.y == -1 ? 2 : 
                            x.border.x == -1 ? 3 :
                            x.border.x == 1 ? 1 :0);
                        var c = (X:x.CellId2D.X * board.CubeFaceSize, Y:x.CellId2D.Y * board.CubeFaceSize);
                        pos2D = (c.X+posInFace.X, c.Y+ posInFace.Y);
                        if (rotation == OrientationName.Left)
                            pos2D = (c.X + board.CubeFaceSize - posInFace.X - 1, c.Y + board.CubeFaceSize - posInFace.Y - 1);
                        if (rotation == OrientationName.Up)
                            pos2D = (c.X + posInFace.Y, c.Y + board.CubeFaceSize - posInFace.X - 1);
                        if (rotation == OrientationName.Down)
                            pos2D = (c.X + board.CubeFaceSize - posInFace.Y - 1, c.Y + posInFace.X);
                        orientationName = (OrientationName)(((int)orientationName+(int)rotation)%4);
                        break;
                    }
                    else
                    {
                        explored.Add(x.CellId2D);
                        bfs.Enqueue(((x.CellId2D.X + dir2D.X, x.CellId2D.Y + dir2D.Y), Rot3DFace90YClockWise(x.border), Rot3DFace90YClockWise(x.face)));
                        bfs.Enqueue(((x.CellId2D.X - dir2D.X, x.CellId2D.Y - dir2D.Y), Rot3DFace90YAntiClockWise(x.border), Rot3DFace90YAntiClockWise(x.face)));
                        bfs.Enqueue(((x.CellId2D.X + dir2D.Y, x.CellId2D.Y - dir2D.X), Rot3DFace90XClockWise(x.border), Rot3DFace90XClockWise(x.face)));
                        bfs.Enqueue(((x.CellId2D.X - dir2D.Y, x.CellId2D.Y + dir2D.X), Rot3DFace90XAntiClockWise(x.border), Rot3DFace90XAntiClockWise(x.face)));
                    }
                }
            }
            return (pos2D, orientationName);
        }

        private static void DisplayMap(Board board)
        {
            if (board.CubeFaceSize == 50)
                return;
            var sb = new StringBuilder();
            for (var row = 0; row<board.Map.Length;row++)
            {
                for (var col = 0; col < board.Map[row].Length; col++)
                {
                    if (board.PositionOnMap == (col, row))
                        sb.Append(
                            board.OrientationName == OrientationName.Right ? '>' :
                            board.OrientationName == OrientationName.Left ? '<' :
                            board.OrientationName == OrientationName.Up ? '^' : 'v'
                            );
                    else
                        sb.Append(board.Map[row][col]);
                }
                sb.Append('\n');
            }
            Debug.WriteLine(sb.ToString());
        }
    }
}