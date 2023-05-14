using System.Text.RegularExpressions;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(22, "Monkey Map")]
    public class MonkeyMap : IPuzzleSolver
    {
        public string SolveFirstPart(string inp)
        {
            var input = inp.Split("\n");
            var inputCommands = input[^1];
            var r = new Regex(@"(\d+)([L|R|E])");
            var commands = r.Matches(inputCommands + "E").Select(x => (Move: int.Parse(x.Groups[1].Value), Rotate: x.Groups[2].Value)).ToList();
            var rotations = new List<(int dx, int dy)>()
    {
        (1,0),
        (0,1),
        (-1,0),
        (0,-1)
    };
            var maxY = input.Length - 2;
            var facing = 0;
            var (x, y) = (input[0].IndexOf('.') + 1, 1);
            var grid = (int x, int y) => input[y - 1][x - 1];
            foreach (var (move, rot) in commands)
            {
                for (var c = 0; c < move; c++)
                {
                    var (dx, dy) = rotations[facing];
                    var (nx, ny) = (x + dx, y + dy);
                    if (dy == 1)
                    {
                        if (ny > maxY || nx > input[ny - 1].Length || grid(nx, ny) == ' ')
                        {
                            ny = 1;
                            while (grid(nx, ny) == ' ')
                                ny++;
                        }
                    }
                    else if (dy == -1)
                    {
                        if (ny < 1 || nx > input[ny - 1].Length || grid(nx, ny) == ' ')
                        {
                            ny = maxY;
                            while (nx > input[ny - 1].Length || grid(nx, ny) == ' ')
                                ny--;
                        }
                    }
                    var maxX = input[ny - 1].Length;
                    if (dx == 1)
                    {
                        if (nx > maxX || grid(nx, ny) == ' ')
                        {
                            nx = 1;
                            while (grid(nx, ny) == ' ')
                                nx++;
                        }
                    }
                    else if (dx == -1)
                    {
                        if (nx < 1 || grid(nx, ny) == ' ')
                        {
                            nx = maxX;
                            while (grid(nx, ny) == ' ')
                                nx--;
                        }
                    }
                    var tile = grid(nx, ny);
                    if (tile == '#') break;
                    (x, y) = (nx, ny);
                }
                if (rot == "R")
                    facing = (facing + 1) % 4;
                if (rot == "L")
                    facing = (facing - 1 + 4) % 4;
            }
            Console.WriteLine($"({x},{y}) {facing}");
            var score = 1000 * y + 4 * x + facing;
            return $"SCORE {score}";
        }
        public string SolveSecondPart(string inp)
        {
            var input = inp.Split("\n");
            var inputCommands = input[^1];
            var r = new Regex(@"(\d+)([L|R|E])");
            var commands = r.Matches(inputCommands + "E").Select(x => (Move: int.Parse(x.Groups[1].Value), Rotate: x.Groups[2].Value)).ToList();
            var rotations = new List<(int dx, int dy)>()
    {
        (1,0),
        (0,1),
        (-1,0),
        (0,-1)
    };
            var cubeFaceSize = 50;
            var cubeFaces = new (int x, int y)[] {
        (1,0),
        (2,0),
        (1,1),
        (0,2),
        (1,2),
        (0,3)
    };
            // borders are 0 right, 1 down, 2 left, 3 up
            var connexions = new List<(int face1, int face2, int border1, int border2)>()
    {
        (0,3,2,2),
        (3,0,2,2),
        (0,5,3,2),
        (5,0,2,3),
        (1,4,0,0),
        (4,1,0,0),
        (1,2,1,0),
        (2,1,0,1),
        (1,5,3,1),
        (5,1,1,3),
        (2,3,2,3),
        (3,2,3,2),
        (4,5,1,0),
        (5,4,0,1)
    };

            var maxY = input.Length - 2;
            var facing = 0;
            var (x, y) = (input[0].IndexOf('.') + 1, 1);
            var grid = (int x, int y) => input[y - 1][x - 1];
            var rotateRight = (int facing) => (facing + 1) % 4;
            var rotateLeft = (int facing) => (facing - 1 + 4) % 4;
            var cmdCount = 0;
            foreach (var (move, rot) in commands)
            {
                cmdCount++;
                for (var c = 0; c < move; c++)
                {
                    var (dx, dy) = rotations[facing];
                    var faceCoord = (x: (x - 1) / cubeFaceSize, y: (y - 1) / cubeFaceSize);
                    var (nx, ny) = (x + dx, y + dy);
                    var newFaceCoord = (x: nx == 0 ? -1 : (nx - 1) / cubeFaceSize, y: ny == 0 ? -1 : (ny - 1) / cubeFaceSize);
                    var nfacing = facing;
                    if (ny > maxY || ny < 1 || nx > input[ny - 1].Length || nx < 1 || grid(nx, ny) == ' ')
                    {
                        var face1 = Array.IndexOf(cubeFaces, faceCoord);
                        var border =
                            newFaceCoord.x - faceCoord.x == 1 ? 0 :
                            newFaceCoord.x - faceCoord.x == -1 ? 2 :
                            newFaceCoord.y - faceCoord.y == 1 ? 1 : 3;
                        var connection = connexions.Single(x => x.face1 == face1 && x.border1 == border);
                        newFaceCoord = cubeFaces[connection.face2];
                        nfacing = (connection.border2 + 2) % 4;
                        var (lx, ly) = ((x - 1) % cubeFaceSize, (y - 1) % cubeFaceSize);
                        var rightBorder = (newFaceCoord.x + 1) * cubeFaceSize;
                        var lowBorder = (newFaceCoord.y + 1) * cubeFaceSize;
                        var leftBorder = newFaceCoord.x * cubeFaceSize + 1;
                        var topBorder = newFaceCoord.y * cubeFaceSize + 1;
                        if (connection.border1 == 2 && connection.border2 == 2)
                            (nx, ny) = (leftBorder, lowBorder - ly);
                        else if (connection.border1 == 3 && connection.border2 == 2)
                            (nx, ny) = (leftBorder, topBorder + lx);
                        else if (connection.border1 == 2 && connection.border2 == 3)
                            (nx, ny) = (leftBorder + ly, topBorder);
                        else if (connection.border1 == 0 && connection.border2 == 0)
                            (nx, ny) = (rightBorder, lowBorder - ly);
                        else if (connection.border1 == 1 && connection.border2 == 0)
                            (nx, ny) = (rightBorder, topBorder + lx);
                        else if (connection.border1 == 0 && connection.border2 == 1)
                            (nx, ny) = (leftBorder + ly, lowBorder);
                        else if (connection.border1 == 3 && connection.border2 == 1)
                            (nx, ny) = (leftBorder + lx, lowBorder);
                        else if (connection.border1 == 1 && connection.border2 == 3)
                            (nx, ny) = (leftBorder + lx, topBorder);
                        else if (connection.border1 == 2 && connection.border2 == 3)
                            (nx, ny) = (leftBorder + ly, topBorder);
                        else if (connection.border1 == 3 && connection.border2 == 2)
                            (nx, ny) = (leftBorder, topBorder + lx);
                        else
                            throw new NotImplementedException();
                    }
                    var tile = grid(nx, ny);
                    if (tile == '#') break;
                    (x, y) = (nx, ny);
                    facing = nfacing;
                }
                if (rot == "R")
                    facing = rotateRight(facing);
                if (rot == "L")
                    facing = rotateLeft(facing);
            }
            Console.WriteLine($"({x},{y}) {facing}");
            var score = 1000 * y + 4 * x + facing;
            return $"SCORE {score}";
        }
    }
}