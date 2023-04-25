namespace AdventOfCode2022web.Domain.Puzzle
{
    public class TreetopTreeHouse : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            var input = Input.Split("\n");
            var gridWidth = input[0].Length;
            var gridHeight = input.Length;
            var explored = new HashSet<(int, int)>();
            var getGrid = (int x, int y) => (int)input[y][x] - (int)'0';
            foreach (var y in Enumerable.Range(0, gridHeight))
            {
                // left to right
                var hmax = -1;
                foreach (var x in Enumerable.Range(0, gridWidth))
                {
                    var h = getGrid(x, y);
                    if (h > hmax)
                    {
                        hmax = h;
                        if (!explored.Contains((x, y))) explored.Add((x, y));
                    }
                }
                // right to left
                hmax = -1;
                foreach (var x in Enumerable.Range(0, gridWidth).Reverse())
                {
                    var h = getGrid(x, y);
                    if (h > hmax)
                    {
                        hmax = h;
                        if (!explored.Contains((x, y))) explored.Add((x, y));
                    }
                }
            }
            foreach (var x in Enumerable.Range(0, gridWidth))
            {
                // top to down
                var hmax = -1;
                foreach (var y in Enumerable.Range(0, gridHeight))
                {
                    var h = getGrid(x, y);
                    if (h > hmax)
                    {
                        hmax = h;
                        if (!explored.Contains((x, y))) explored.Add((x, y));
                    }
                }
                // down to top
                hmax = -1;
                foreach (var y in Enumerable.Range(0, gridHeight).Reverse())
                {
                    var h = getGrid(x, y);
                    if (h > hmax)
                    {
                        hmax = h;
                        if (!explored.Contains((x, y))) explored.Add((x, y));
                    }
                }
            }
            Console.WriteLine(explored.Count);
            return explored.Count.ToString();
        }
        public string Part2()
        {
            var input = Input.Split("\n");
            var gridWidth = input[0].Length;
            var gridHeight = input.Length;
            var explored = new HashSet<(int, int)>();
            var getGrid = (int x, int y) => (int)input[y][x] - (int)'0';
            var directions = new List<(int, int)> { (1, 0), (-1, 0), (0, 1), (0, -1) };
            var scoreMax = 0;
            var borderReached = (int x, int y) => x < 0 || x >= gridWidth || y < 0 || y >= gridHeight;
            foreach (var y in Enumerable.Range(0, gridHeight))
                foreach (var x in Enumerable.Range(0, gridWidth))
                {
                    var h = getGrid(x, y);
                    var score = 1;
                    foreach (var (dx, dy) in directions)
                    {
                        var i = 1;
                        var (nx, ny) = (x, y);
                        do
                        {
                            (nx, ny) = (x + i * dx, y + i * dy);
                            if (borderReached(nx, ny)) break;
                            i++;
                        } while (getGrid(nx, ny) < h);
                        score *= (i - 1);
                    }
                    scoreMax = score > scoreMax ? score : scoreMax;
                }
            Console.WriteLine(scoreMax);
            return scoreMax.ToString();
        }
    }
    public class SolveDay9 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay10 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay11 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay12 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay13 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay14 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay15 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay16 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay17 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay18 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay19 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay20 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay21 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay22 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay23 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay24 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
    public class SolveDay25 : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            return "";
        }
        public string Part2()
        {
            return "";
        }
    }
}