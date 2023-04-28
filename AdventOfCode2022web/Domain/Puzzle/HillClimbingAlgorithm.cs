namespace AdventOfCode2022web.Domain.Puzzle
{
    public class HillClimbingAlgorithm : IPuzzleSolver
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
            var directions = new List<(int, int)> { (1, 0), (-1, 0), (0, 1), (0, -1) };
            var start = (x: 0, y: 0);
            foreach (var l in input)
            {
                var s = l.IndexOf('S');
                if (s != -1)
                {
                    start.x = s;
                    break;
                }
                start.y++;
            }
            var explored = new HashSet<(int, int)>() { start };
            var queue = new Queue<(int, int)>();
            queue.Enqueue(start);
            var getH = (char c) => { if (c == 'S') c = 'a'; if (c == 'E') c = 'z'; return (int)c - (int)'a'; };
            var score = 0;
            while (queue.Count > 0)
            {
                score++;
                var newQueue = new Queue<(int, int)>();
                while (queue.Count > 0)
                {
                    var (x, y) = queue.Dequeue();
                    var c = input[y][x];
                    var h = getH(c);
                    foreach (var (dx, dy) in directions)
                    {
                        var ne = (x: x + dx, y: y + dy);
                        if (ne.x < 0 || ne.x >= gridWidth || ne.y < 0 || ne.y >= gridHeight || explored.Contains(ne)) continue;
                        var nc = input[ne.y][ne.x];
                        var nh = getH(nc);
                        if (nh - h < 2)
                        {
                            newQueue.Enqueue(ne);
                            explored.Add(ne);
                            if (nc == 'E') { newQueue.Clear(); queue.Clear(); break; }
                        }
                    }
                }
                queue = newQueue;
            }
            Console.WriteLine(explored.Count);
            return score.ToString();
        }
        public string Part2()
        {
            var input = Input.Split("\n");
            var gridWidth = input[0].Length;
            var gridHeight = input.Length;
            var directions = new List<(int, int)> { (1, 0), (-1, 0), (0, 1), (0, -1) };
            var start = (x: 0, y: 0);
            var queue = new Queue<(int, int)>();
            for (var y = 0; y < gridHeight; y++)
                for (var x = 0; x < gridWidth; x++)
                    if (input[y][x] == 'a') queue.Enqueue((x, y));
            var explored = new HashSet<(int, int)>() { start };
            queue.Enqueue(start);
            var getH = (char c) => { if (c == 'S') c = 'a'; if (c == 'E') c = 'z'; return (int)c - (int)'a'; };
            var score = 0;
            while (queue.Count > 0)
            {
                score++;
                var newQueue = new Queue<(int, int)>();
                while (queue.Count > 0)
                {
                    var (x, y) = queue.Dequeue();
                    var c = input[y][x];
                    var h = getH(c);
                    foreach (var (dx, dy) in directions)
                    {
                        var ne = (x: x + dx, y: y + dy);
                        if (ne.x < 0 || ne.x >= gridWidth || ne.y < 0 || ne.y >= gridHeight || explored.Contains(ne)) continue;
                        var nc = input[ne.y][ne.x];
                        var nh = getH(nc);
                        if (nh - h < 2)
                        {
                            newQueue.Enqueue(ne);
                            explored.Add(ne);
                            if (nc == 'E') { newQueue.Clear(); queue.Clear(); break; }
                        }
                    }
                }
                queue = newQueue;
            }
            Console.WriteLine(explored.Count);
            return score.ToString();
        }
    }
}