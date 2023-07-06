namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(24, "Blizzard Basin")]
    public class BlizzardBasin : IPuzzleSolverV3
    {
        public string Visualize()
        {
            return string.Empty;
        }

        public void Setup(string puzzleInput)
        {
            var input = puzzleInput.Split("\n");
            Start = (x: input[0].IndexOf('.'), y: 0);
            Arrival = (x: input[^1].IndexOf('.'), y: input.Length - 1);
            Walls = input
                .SelectMany((line, row) => line.Select((c, col) => (c, col, row))
                .Where(y => y.c == '#'))
                .Select(e => (x: e.col, y: e.row))
                .ToHashSet();
            var blizzardsTypes = new char[] { '>', '<', '^', 'v' };
            var blizzards = input
                .SelectMany((line, row) => line.Select((c, col) => (c, col, row))
                .Where(y => blizzardsTypes.Contains(y.c)))
                .Select(e => (x: e.col, y: e.row, e.c))
                .ToList();
            BlizzardsRight = blizzards.Where(e => e.c == '>').ToList();
            BlizzardsLeft = blizzards.Where(e => e.c == '<').ToList();
            BlizzardsUp = blizzards.Where(e => e.c == '^').ToList();
            BlizzardsDown = blizzards.Where(e => e.c == 'v').ToList();
            Width = input[0].Length - 2;
            Height = input.Length - 2;
        }

        private (int x, int y) Start;
        private (int x, int y) Arrival;
        private List<(int x, int y, char c)> BlizzardsRight;
        private List<(int x, int y, char c)> BlizzardsLeft;
        private List<(int x, int y, char c)> BlizzardsUp;
        private List<(int x, int y, char c)> BlizzardsDown;
        private int Width;
        private int Height;
        private HashSet<(int x, int y)> Walls;

        private static readonly List<(int dx, int dy)> Moves = new List<(int dx, int dy)>()
        {
            (0,0),
            (1,0),
            (-1,0),
            (0,1),
            (0,-1)
        };


        public IEnumerable<string> SolveFirstPart()
        {
            var minute = 0;
            var search = new Queue<(int x, int y)>();
            var mod = (int x, int m) => (x % m + m) % m;
            search.Enqueue(Start);
            do
            {
                minute++;
                var newSearch = new Queue<(int x, int y)>();
                // compute blizzards positions
                var blizzardsPos = BlizzardsRight.Select(e => ((e.x - 1 + minute) % Width + 1, e.y)).ToHashSet();
                blizzardsPos.UnionWith(BlizzardsLeft.Select(e => (mod(e.x - 1 - minute, Width) + 1, e.y)));
                blizzardsPos.UnionWith(BlizzardsUp.Select(e => (e.x, mod(e.y - 1 - minute, Height) + 1)));
                blizzardsPos.UnionWith(BlizzardsDown.Select(e => (e.x, (e.y - 1 + minute) % Height + 1)));
                while (search.TryDequeue(out var expedition))
                {
                    foreach (var (dx, dy) in Moves)
                    {
                        var pos = (x: expedition.x + dx, y: expedition.y + dy);
                        if (pos == Arrival)
                        {
                            yield return $"FOUND {minute}";
                            yield break;
                        }
                        if (pos.y >= 0 && !blizzardsPos.Contains(pos) && !Walls.Contains(pos) && !newSearch.Contains(pos))
                            newSearch.Enqueue(pos);
                    }
                }
                search = newSearch;
            } while (search.Count > 0);
            yield return $"NOT FOUND {minute}";
        }
        public IEnumerable<string> SolveSecondPart()
        {
            var minute = 0;
            var search = new Queue<(int x, int y)>();
            var mod = (int x, int m) => (x % m + m) % m;
            search.Enqueue(Start);
            do
            {
                minute++;
                var newSearch = new Queue<(int x, int y)>();
                // compute blizzards positions
                var blizzardsPos = BlizzardsRight.Select(e => ((e.x - 1 + minute) % Width + 1, e.y)).ToHashSet();
                blizzardsPos.UnionWith(BlizzardsLeft.Select(e => (mod(e.x - 1 - minute, Width) + 1, e.y)));
                blizzardsPos.UnionWith(BlizzardsUp.Select(e => (e.x, mod(e.y - 1 - minute, Height) + 1)));
                blizzardsPos.UnionWith(BlizzardsDown.Select(e => (e.x, (e.y - 1 + minute) % Height + 1)));
                while (search.TryDequeue(out var expedition))
                {
                    foreach (var (dx, dy) in Moves)
                    {
                        var pos = (x: expedition.x + dx, y: expedition.y + dy);
                        if (pos == Arrival)
                        {
                            Console.WriteLine($"FOUND {minute}");
                            newSearch.Clear();
                            search.Clear();
                            break;
                        }
                        if (pos.y >= 0 && !blizzardsPos.Contains(pos) && !Walls.Contains(pos) && !newSearch.Contains(pos))
                            newSearch.Enqueue(pos);
                    }
                }
                search = newSearch;
            } while (search.Count > 0);
            Console.WriteLine($"SEARCH 1 completed {minute}");
            search.Enqueue(Arrival);
            do
            {
                minute++;
                var newSearch = new Queue<(int x, int y)>();
                // compute blizzards positions
                var blizzardsPos = BlizzardsRight.Select(e => ((e.x - 1 + minute) % Width + 1, e.y)).ToHashSet();
                blizzardsPos.UnionWith(BlizzardsLeft.Select(e => (mod(e.x - 1 - minute, Width) + 1, e.y)));
                blizzardsPos.UnionWith(BlizzardsUp.Select(e => (e.x, mod(e.y - 1 - minute, Height) + 1)));
                blizzardsPos.UnionWith(BlizzardsDown.Select(e => (e.x, (e.y - 1 + minute) % Height + 1)));
                while (search.TryDequeue(out var expedition))
                {
                    foreach (var (dx, dy) in Moves)
                    {
                        var pos = (x: expedition.x + dx, y: expedition.y + dy);
                        if (pos == Start)
                        {
                            Console.WriteLine($"FOUND {minute}");
                            newSearch.Clear();
                            search.Clear();
                            break;
                        }
                        if (pos.y >= 0 && pos.y < Height+2 && !blizzardsPos.Contains(pos) && !Walls.Contains(pos) && !newSearch.Contains(pos))
                            newSearch.Enqueue(pos);
                    }
                }
                search = newSearch;
            } while (search.Count > 0);
            Console.WriteLine($"SEARCH 2 completed {minute}");
            search.Enqueue(Start);
            do
            {
                minute++;
                var newSearch = new Queue<(int x, int y)>();
                // compute blizzards positions
                var blizzardsPos = BlizzardsRight.Select(e => ((e.x - 1 + minute) % Width + 1, e.y)).ToHashSet();
                blizzardsPos.UnionWith(BlizzardsLeft.Select(e => (mod(e.x - 1 - minute, Width) + 1, e.y)));
                blizzardsPos.UnionWith(BlizzardsUp.Select(e => (e.x, mod(e.y - 1 - minute, Height) + 1)));
                blizzardsPos.UnionWith(BlizzardsDown.Select(e => (e.x, (e.y - 1 + minute) % Height + 1)));
                while (search.TryDequeue(out var expedition))
                {
                    foreach (var (dx, dy) in Moves)
                    {
                        var pos = (x: expedition.x + dx, y: expedition.y + dy);
                        if (pos == Arrival)
                        {
                            Console.WriteLine($"FOUND {minute}");
                            newSearch.Clear();
                            search.Clear();
                            break;
                        }
                        if (pos.y >= 0 && !blizzardsPos.Contains(pos) && !Walls.Contains(pos) && !newSearch.Contains(pos))
                            newSearch.Enqueue(pos);
                    }
                }
                search = newSearch;
            } while (search.Count > 0);
            yield return $"SEARCH 3 completed {minute}";
        }

    }
}