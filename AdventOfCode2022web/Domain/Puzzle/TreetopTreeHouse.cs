namespace AdventOfCode2022web.Domain.Puzzle
{
    public class TreetopTreeHouse : IPuzzleSolver
    {
        public async IAsyncEnumerable<string> Part1Async(string input)
        {
            Input = input;
            yield return Part1();
            await Task.Delay(1);
        }
        public async IAsyncEnumerable<string> Part2Async(string input)
        {
            Input = input;
            yield return Part2();
            await Task.Delay(1);
        }

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
}