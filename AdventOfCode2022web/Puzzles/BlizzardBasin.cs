namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(24, "Blizzard Basin")]
    public class BlizzardBasin : IPuzzleSolver
    {
        public IEnumerable<string> SolveFirstPart(string inp)
        {
            var input = inp.Split("\n");
            var start = (x: input[0].IndexOf('.'), y: 0);
            var arrival = (x: input[^1].IndexOf('.'), y: input.Length - 1);
            var walls = input
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
            var blizzardsRight = blizzards.Where(e => e.c == '>').ToList();
            var blizzardsLeft = blizzards.Where(e => e.c == '<').ToList();
            var blizzardsUp = blizzards.Where(e => e.c == '^').ToList();
            var blizzardsDown = blizzards.Where(e => e.c == 'v').ToList();
            var width = input[0].Length - 2;
            var height = input.Length - 2;

            var minute = 0;
            var search = new Queue<(int x, int y)>();
            var moves = new List<(int dx, int dy)>()
    {
        (0,0),
        (1,0),
        (-1,0),
        (0,1),
        (0,-1)
    };
            var mod = (int x, int m) => (x % m + m) % m;
            search.Enqueue(start);
            do
            {
                minute++;
                var newSearch = new Queue<(int x, int y)>();
                // compute blizzards positions
                var blizzardsPos = blizzardsRight.Select(e => ((e.x - 1 + minute) % width + 1, e.y)).ToHashSet();
                blizzardsPos.UnionWith(blizzardsLeft.Select(e => (mod(e.x - 1 - minute, width) + 1, e.y)));
                blizzardsPos.UnionWith(blizzardsUp.Select(e => (e.x, mod(e.y - 1 - minute, height) + 1)));
                blizzardsPos.UnionWith(blizzardsDown.Select(e => (e.x, (e.y - 1 + minute) % height + 1)));
                while (search.TryDequeue(out var expedition))
                {
                    foreach (var (dx, dy) in moves)
                    {
                        var pos = (x: expedition.x + dx, y: expedition.y + dy);
                        if (pos == arrival)
                        {
                            yield return $"FOUND {minute}";
                            yield break;
                        }
                        if (pos.y >= 0 && !blizzardsPos.Contains(pos) && !walls.Contains(pos) && !newSearch.Contains(pos))
                            newSearch.Enqueue(pos);
                    }
                }
                search = newSearch;
            } while (search.Count > 0);
            yield return $"NOT FOUND {minute}";
        }
        public IEnumerable<string> SolveSecondPart(string inp)
        {
            var input = inp.Split("\n");
            var start = (x: input[0].IndexOf('.'), y: 0);
            var arrival = (x: input[^1].IndexOf('.'), y: input.Length - 1);
            var walls = input
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
            var blizzardsRight = blizzards.Where(e => e.c == '>').ToList();
            var blizzardsLeft = blizzards.Where(e => e.c == '<').ToList();
            var blizzardsUp = blizzards.Where(e => e.c == '^').ToList();
            var blizzardsDown = blizzards.Where(e => e.c == 'v').ToList();
            var width = input[0].Length - 2;
            var height = input.Length - 2;

            var minute = 0;
            var search = new Queue<(int x, int y)>();
            var moves = new List<(int dx, int dy)>()
    {
        (0,0),
        (1,0),
        (-1,0),
        (0,1),
        (0,-1)
    };
            var mod = (int x, int m) => (x % m + m) % m;
            search.Enqueue(start);
            do
            {
                minute++;
                var newSearch = new Queue<(int x, int y)>();
                // compute blizzards positions
                var blizzardsPos = blizzardsRight.Select(e => ((e.x - 1 + minute) % width + 1, e.y)).ToHashSet();
                blizzardsPos.UnionWith(blizzardsLeft.Select(e => (mod(e.x - 1 - minute, width) + 1, e.y)));
                blizzardsPos.UnionWith(blizzardsUp.Select(e => (e.x, mod(e.y - 1 - minute, height) + 1)));
                blizzardsPos.UnionWith(blizzardsDown.Select(e => (e.x, (e.y - 1 + minute) % height + 1)));
                while (search.TryDequeue(out var expedition))
                {
                    foreach (var (dx, dy) in moves)
                    {
                        var pos = (x: expedition.x + dx, y: expedition.y + dy);
                        if (pos == arrival)
                        {
                            Console.WriteLine($"FOUND {minute}");
                            newSearch.Clear();
                            search.Clear();
                            break;
                        }
                        if (pos.y >= 0 && !blizzardsPos.Contains(pos) && !walls.Contains(pos) && !newSearch.Contains(pos))
                            newSearch.Enqueue(pos);
                    }
                }
                search = newSearch;
            } while (search.Count > 0);
            Console.WriteLine($"SEARCH 1 completed {minute}");
            search.Enqueue(arrival);
            do
            {
                minute++;
                var newSearch = new Queue<(int x, int y)>();
                // compute blizzards positions
                var blizzardsPos = blizzardsRight.Select(e => ((e.x - 1 + minute) % width + 1, e.y)).ToHashSet();
                blizzardsPos.UnionWith(blizzardsLeft.Select(e => (mod(e.x - 1 - minute, width) + 1, e.y)));
                blizzardsPos.UnionWith(blizzardsUp.Select(e => (e.x, mod(e.y - 1 - minute, height) + 1)));
                blizzardsPos.UnionWith(blizzardsDown.Select(e => (e.x, (e.y - 1 + minute) % height + 1)));
                while (search.TryDequeue(out var expedition))
                {
                    foreach (var (dx, dy) in moves)
                    {
                        var pos = (x: expedition.x + dx, y: expedition.y + dy);
                        if (pos == start)
                        {
                            Console.WriteLine($"FOUND {minute}");
                            newSearch.Clear();
                            search.Clear();
                            break;
                        }
                        if (pos.y >= 0 && pos.y < input.Length && !blizzardsPos.Contains(pos) && !walls.Contains(pos) && !newSearch.Contains(pos))
                            newSearch.Enqueue(pos);
                    }
                }
                search = newSearch;
            } while (search.Count > 0);
            Console.WriteLine($"SEARCH 2 completed {minute}");
            search.Enqueue(start);
            do
            {
                minute++;
                var newSearch = new Queue<(int x, int y)>();
                // compute blizzards positions
                var blizzardsPos = blizzardsRight.Select(e => ((e.x - 1 + minute) % width + 1, e.y)).ToHashSet();
                blizzardsPos.UnionWith(blizzardsLeft.Select(e => (mod(e.x - 1 - minute, width) + 1, e.y)));
                blizzardsPos.UnionWith(blizzardsUp.Select(e => (e.x, mod(e.y - 1 - minute, height) + 1)));
                blizzardsPos.UnionWith(blizzardsDown.Select(e => (e.x, (e.y - 1 + minute) % height + 1)));
                while (search.TryDequeue(out var expedition))
                {
                    foreach (var (dx, dy) in moves)
                    {
                        var pos = (x: expedition.x + dx, y: expedition.y + dy);
                        if (pos == arrival)
                        {
                            Console.WriteLine($"FOUND {minute}");
                            newSearch.Clear();
                            search.Clear();
                            break;
                        }
                        if (pos.y >= 0 && !blizzardsPos.Contains(pos) && !walls.Contains(pos) && !newSearch.Contains(pos))
                            newSearch.Enqueue(pos);
                    }
                }
                search = newSearch;
            } while (search.Count > 0);
            yield return $"SEARCH 3 completed {minute}";
        }

    }
}