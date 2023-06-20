using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(22, "Monkey Map")]
    public class MonkeyMap : IPuzzleSolver
    {
        private struct Board
        {
            public string[] MapOfStrangelyShapedBoard;
            public List<(int Move, string Rotation)> Instructions;
            public (int X, int Y) Position;
            public Direction Direction;
            public int MaxY => MapOfStrangelyShapedBoard.Length - 1;
            public int MaxX => MapOfStrangelyShapedBoard[Position.Y].Length - 1;
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
                MapOfStrangelyShapedBoard = input,
                Instructions = instructions,
                Position = (input[0].IndexOf('.'), 0),
                Direction = Direction.Right,
                CubeFaceSize = cubeFaceSize
            };
        }
        private enum Direction
        {
            Right = 0,
            Down = 1,
            Left = 2,
            Up = 3
        }


        private static char QueryMap(Board board, (int X, int Y) pos)
        {
            if (pos.X < 0 || pos.Y < 0 || pos.Y > board.MaxY || pos.X > board.MapOfStrangelyShapedBoard[pos.Y].Length - 1)
                return ' ';
            else
                return board.MapOfStrangelyShapedBoard[pos.Y][pos.X];
        }

        private static bool IsWallBlockingOnCube(Board board)
        {
            var (pos, _) = ComputeNextPositionOnCube(board);
            return QueryMap(board, pos) == '#';
        }

        private static bool IsWallBlockingOnDevelopedCube(Board board)
        {
            var pos = ComputeNextPositionOnDevelopedCube(board);
            return QueryMap(board, pos) == '#';
        }

        private static ((int X, int Y) Position, Direction D) ComputeNextPositionOnCube(Board board)
        {
            var pos = board.Position;
            var direction = board.Direction;
            var currentCubeFace = (X: (pos.X / board.CubeFaceSize) % 4, Y: pos.Y / board.CubeFaceSize);
            if (board.Direction == Direction.Right)
                pos.X += 1;
            if (board.Direction == Direction.Left)
                pos.X -= 1;
            if (board.Direction == Direction.Down)
                pos.Y += 1;
            if (board.Direction == Direction.Up)
                pos.Y -= 1;
            if (QueryMap(board, pos) == ' ')
            {
                var pathsToAdjacentFace = new Direction[][]
                {
                    // first is the rotation needed, other the traversal on the cube flat map
                    // todo : find the rule between them
                        new Direction[] { Direction.Down,         Direction.Down, Direction.Right },
                        new Direction[] { Direction.Left,       Direction.Down, Direction.Down, Direction.Right },
                        new Direction[] { Direction.Up,       Direction.Up, Direction.Right },
                        new Direction[] { Direction.Left,       Direction.Up, Direction.Up, Direction.Right },
                        new Direction[] { Direction.Right,      Direction.Left, Direction.Left, Direction.Left },
                        new Direction[] { Direction.Left,       Direction.Left, Direction.Down, Direction.Down },
                        new Direction[] { Direction.Left,       Direction.Left, Direction.Up, Direction.Up },
                };
                var posInFace = (X: pos.X % board.CubeFaceSize, Y: pos.Y % board.CubeFaceSize);
                foreach (var traversal in pathsToAdjacentFace)
                {
                    var (X, Y) = currentCubeFace;
                    foreach (var m in traversal.Skip(1))
                    {
                        var move = (Direction)(((int)m + (int)direction) %4);
                        if (move == Direction.Up)
                                Y--;
                        if (move == Direction.Down)
                            Y++;
                        if (move == Direction.Right)
                            X++;
                        if (move == Direction.Left)
                            X--;
                        if (QueryMap(board, (X * board.CubeFaceSize, Y * board.CubeFaceSize)) == ' ')
                            break;
                    }
                    if (QueryMap(board, (X * board.CubeFaceSize, Y * board.CubeFaceSize)) != ' ')
                    {
                        var rotation = traversal.First();
                        if (rotation == Direction.Down)
                            posInFace = (board.CubeFaceSize - posInFace.Y - 1, posInFace.X);
                        if (rotation == Direction.Left)
                            posInFace = (board.CubeFaceSize - posInFace.X - 1, board.CubeFaceSize - posInFace.Y - 1);
                        if (rotation == Direction.Up)
                            posInFace = (posInFace.Y, board.CubeFaceSize - posInFace.X - 1);
                        direction = (Direction)(((int)direction + (int)rotation) % 4);
                        pos = (X * board.CubeFaceSize + posInFace.X, Y * board.CubeFaceSize + posInFace.Y);
                        break;
                    } // todo no error if nothing found
                }
            }
            return (pos, direction);
        }

        private static (int X, int Y) ComputeNextPositionOnDevelopedCube(Board board)
        {
            var pos = board.Position;
            if (board.Direction == Direction.Right)
            {
                while (true)
                {
                    pos.X += 1;
                    if (pos.X > board.MaxX)
                        pos.X = 0;
                    if (QueryMap(board, pos) != ' ')
                        break;
                }
            }
            if (board.Direction == Direction.Left)
            {
                while (true)
                {
                    pos.X -= 1;
                    if (pos.X < 1)
                        pos.X = board.MaxX;
                    if (QueryMap(board, pos) != ' ')
                        break;
                }
            }
            if (board.Direction == Direction.Down)
            {
                while (true)
                {
                    pos.Y += 1;
                    if (pos.Y > board.MaxY)
                        pos.Y = 0;
                    if (QueryMap(board, pos) != ' ')
                        break;
                }
            }
            if (board.Direction == Direction.Up)
            {
                while (true)
                {
                    pos.Y -= 1;
                    if (pos.Y < 1)
                        pos.Y = board.MaxY;
                    if (QueryMap(board, pos) != ' ')
                        break;
                }
            }
            return pos;
        }

        public string SolveFirstPart(string inp)
        {
            var strangelyShapedBoard = ProcessInput(inp);
            foreach (var (move, rotation) in strangelyShapedBoard.Instructions)
            {
                Debug.WriteLine($"{move} {rotation}");
                for (var step = 0; step < move; step++)
                {
                    if (IsWallBlockingOnDevelopedCube(strangelyShapedBoard))
                        break;
                    strangelyShapedBoard.Position = ComputeNextPositionOnDevelopedCube(strangelyShapedBoard);
                }
                if (rotation == "R")
                    strangelyShapedBoard.Direction = (Direction)(((int)strangelyShapedBoard.Direction + 1) % 4);
                if (rotation == "L")
                    strangelyShapedBoard.Direction = (Direction)(((int)strangelyShapedBoard.Direction - 1 + 4) % 4);
            }
            return (1000 * (strangelyShapedBoard.Position.Y + 1) + 4 * (strangelyShapedBoard.Position.X + 1) + (int)strangelyShapedBoard.Direction).ToString();
        }

        public string SolveSecondPart(string inp)
        {
            var strangelyShapedBoard = ProcessInput(inp);
            foreach (var (move, rotation) in strangelyShapedBoard.Instructions)
            {
                Debug.WriteLine($"{move} {rotation}");
                for (var step = 0; step < move; step++)
                {
                    if (IsWallBlockingOnCube(strangelyShapedBoard))
                        break;
                    (strangelyShapedBoard.Position, strangelyShapedBoard.Direction) = ComputeNextPositionOnCube(strangelyShapedBoard);
                }
                if (rotation == "R")
                    strangelyShapedBoard.Direction = (Direction)(((int)strangelyShapedBoard.Direction + 1) % 4);
                if (rotation == "L")
                    strangelyShapedBoard.Direction = (Direction)(((int)strangelyShapedBoard.Direction - 1 + 4) % 4);
            }
            return (1000 * (strangelyShapedBoard.Position.Y+1) + 4 * (strangelyShapedBoard.Position.X+1) + (int)strangelyShapedBoard.Direction).ToString();
        }
    }
}