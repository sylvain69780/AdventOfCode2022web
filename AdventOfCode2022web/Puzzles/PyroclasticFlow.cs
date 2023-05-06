namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(17, "Pyroclastic Flow")]
    public class PyroclasticFlow : IPuzzleSolver
    {
        public IEnumerable<string> SolveFirstPart(string inp)
        {
            var input = inp;
            var inputs = input.Length;
            var blocks = new List<(int, int)>[]
            {
        new List<(int,int)> { (0,0) , (1,0), (2,0), (3,0)},
        new List<(int,int)> { (1,0) , (0,1), (1,1), (2,1), (1,2)},
        new List<(int,int)> { (0,0) , (1,0), (2,0), (2,1), (2,2)},
        new List<(int,int)> { (0,0) , (0,1), (0,2), (0,3)},
        new List<(int,int)> { (0,0) , (1,0), (0,1), (1,1)}
            };

            var grid = new HashSet<(int, int)>();
            var jetSequ = 0;
            var rockSequ = 0;
            var jetFunc = () => { var r = input[jetSequ]; jetSequ = (jetSequ + 1) % inputs; return r; };
            var rockFunc = () => { var r = blocks[rockSequ]; rockSequ = (rockSequ + 1) % 5; return r; };
            for (var i = 0; i <= 2022; i++)
            {
                var rock = rockFunc();
                var stop = false;
                var top = grid.Count == 0 ? 0 : grid.Select(x => x.Item2).Max() + 1;
                Console.WriteLine($"{i}: Tower top is at {top}");
                yield return $"{i}: Tower top is at {top}";

                var (px, py) = (2, top + 3);
                while (!stop)
                {
                    var move = jetFunc() == '>' ? 1 : -1;
                    if (!rock.Select(x => (x.Item1 + px + move, x.Item2 + py)).Any(x => x.Item1 < 0 || x.Item1 >= 7 || grid.Contains(x)))
                        px += move;
                    stop = rock.Select(x => (x.Item1 + px, x.Item2 + py - 1)).Any(x => x.Item2 < 0 || grid.Contains(x));
                    if (!stop) py--;
                    else
                        foreach (var p in rock)
                            grid.Add((p.Item1 + px, p.Item2 + py));
                }
            }
        }
        public IEnumerable<string> SolveSecondPart(string inp)
        {
            var input = inp;
            var inputs = input.Length;
            var blocks = new List<(int, int)>[]
            {
        new List<(int,int)> { (0,0) , (1,0), (2,0), (3,0)},
        new List<(int,int)> { (1,0) , (0,1), (1,1), (2,1), (1,2)},
        new List<(int,int)> { (0,0) , (1,0), (2,0), (2,1), (2,2)},
        new List<(int,int)> { (0,0) , (0,1), (0,2), (0,3)},
        new List<(int,int)> { (0,0) , (1,0), (0,1), (1,1)}
            };

            var grid = new HashSet<(int, int)>();
            var jetSequ = 0;
            var rockSequ = 0;
            var jetFunc = () => { var r = input[jetSequ]; jetSequ = (jetSequ + 1) % inputs; return r; };
            var rockFunc = () => { var r = blocks[rockSequ]; rockSequ = (rockSequ + 1) % 5; return r; };
            var output = new List<string>();
            var heights = new List<int>();
            var outputIdx = new Dictionary<string, int>();
            var found = false;
            var i = 0;
            var top = 0;
            var (start, end) = (0, 1);
            var dist = 1;
            while (!found)
            {
                var rock = rockFunc();
                var stop = false;
                //        Console.WriteLine($"{i}: Tower top is at {top}");
                var (px, py) = (2, 3);
                while (!stop)
                {
                    var (j, r) = (jetSequ, rockSequ);
                    var move = jetFunc() == '>' ? 1 : -1;
                    if (!rock.Select(x => (x.Item1 + px + move, x.Item2 + py + top)).Any(x => x.Item1 < 0 || x.Item1 >= 7 || grid.Contains(x)))
                        px += move;
                    stop = rock.Select(x => (x.Item1 + px, x.Item2 + py + top - 1)).Any(x => x.Item2 < 0 || grid.Contains(x));
                    if (!stop) py--;

                    else
                    {
                        foreach (var p in rock)
                            grid.Add((p.Item1 + px, p.Item2 + py + top));
                        top = grid.Select(x => x.Item2).Max() + 1;
                        var key = $"{r} {j} {px} {py}";
                        heights.Add(top);
                        output.Add(key);
                        // look for cycles
                        if (output.Count > 1)
                        {
                            if (output[i] != output[i - dist])
                            {
                                for (dist = i; dist > 1; dist--)
                                    if (output[i] == output[i - dist]) break;
                                start = i - dist;
                            }
                            else
                            {
                                Console.WriteLine($"{i} starting {start} dist = {dist}.");
                                if (i - start > dist * 2) found = true;
                            }
                        }
                        Console.WriteLine($"{i} key={key} top={top} starting {start} dist = {dist}.");
                        yield return $"{i} key={key} top={top} starting {start} dist = {dist}.";
                    }
                }
                i++;
            }
            long big = 1000000000000;
            big -= 1;
            var v1 = heights[start];
            var v2 = (int)((big - start) % dist);
            var v3 = heights[v2 + start] - heights[start];
            var v4 = (big - start) / dist;
            var v5 = heights[start + dist] - heights[start];
            yield return $"{v1} + {v4}x{v5} + {v3} = {v1 + v4 * v5 + v3}";
        }
    }
}