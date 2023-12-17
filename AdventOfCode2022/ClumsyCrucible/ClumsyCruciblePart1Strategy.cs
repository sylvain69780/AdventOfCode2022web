using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ClumsyCrucible
{
    public class ClumsyCruciblePart1Strategy : IPuzzleStrategy<ClumsyCrucibleModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(ClumsyCrucibleModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var mapstr = model.Map!;
            var width = mapstr[0].Length;
            var height = mapstr.Length;
            var map = new int[width, height];
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                    map[x, y] = mapstr[y][x] - '0';
            var queue = new Queue<(int x, int y, char dir, int count, int heat)>();
            queue.Enqueue((x: 1, y: 0, dir: '>', count: 1, heat: map[1,0]));
            queue.Enqueue((x: 0, y: 1, dir: 'v', count: 1, heat: map[0,1]));
            var bestArrivalHeat = int.MaxValue;
            var backTracking = new Dictionary<(int x, int y, char dir, int count, int heat), (int x, int y, char dir, int count, int heat)>();
            var cache = new Dictionary<(int x, int y, char dir, int count), int>();
            //var seq = ">>v>>>^>>>vv>>vv>vvv>vvv<vv>";
            var i = 0;
            while (queue.Count > 0 && queue.Any(x => x.heat < bestArrivalHeat))
            {
                var newQueue = new Queue<(int x, int y, char dir, int count, int heat)>();
//                var headHeat = queue.Select(p => p.heat + 3 * 9 * (Math.Abs(p.x + 1 - width) + Math.Abs(p.y + 1 - height))).Min();
//                var filter = queue.GroupBy(p => (p.x, p.y, p.dir, p.count), p => p.heat).ToDictionary(x => x.Key, x => x.Min());
                while (queue.TryDequeue(out var p))
                {
                    //if (p.dir != seq[i])
                    //    continue;
                    var d = Math.Abs(p.x + 1 - width) + Math.Abs(p.y + 1 - height);
                    if (d == 0)
                    {
                        if (bestArrivalHeat > p.heat)
                            bestArrivalHeat = p.heat;
                        continue;
                    }
                    var options = new List<(int x, int y, char dir, int count)>();
                    if (p.dir == '>')
                    {
                        options.Add((p.x, p.y + 1, 'v', 1));
                        options.Add((p.x, p.y - 1, '^', 1));
                        if (p.count < 3)
                            options.Add((p.x + 1, p.y, '>', p.count + 1));
                    }
                    else if (p.dir == '<')
                    {
                        options.Add((p.x, p.y + 1, 'v', 1));
                        options.Add((p.x, p.y - 1, '^', 1));
                        if (p.count < 3)
                            options.Add((p.x - 1, p.y, '<', p.count + 1));
                    }
                    else if (p.dir == 'v')
                    {
                        options.Add((p.x + 1, p.y, '>', 1));
                        options.Add((p.x - 1, p.y, '<', 1));
                        if (p.count < 3)
                            options.Add((p.x, p.y + 1, 'v', p.count + 1));
                    }
                    else if (p.dir == '^')
                    {
                        options.Add((p.x + 1, p.y, '>', 1));
                        options.Add((p.x - 1, p.y, '<', 1));
                        if (p.count < 3)
                            options.Add((p.x, p.y - 1, '^', p.count + 1));
                    }
                    foreach (var (x, y, dir, count) in options)
                    {
                        if (x < 0 || x >= width || y < 0 || y >= height)
                            continue;
                        var p2 = (x, y, dir, count, heat:p.heat + map[x,y]);
                        if (cache.TryGetValue((p2.x, p2.y, p2.dir, p2.count), out var previous) && previous <= p2.heat)
                            continue;
                        else
                            cache[(p2.x, p2.y, p2.dir, p2.count)] = p2.heat;
                        if (!backTracking.ContainsKey(p2))
                        {
                            backTracking.Add(p2, p);
                            newQueue.Enqueue(p2);
                        }
                    }
                }
                queue = newQueue;
                i++;
            }
            var solution = backTracking.First(p => p.Key.heat == bestArrivalHeat && p.Key.x == width-1 && p.Key.y == height- 1);
            var backtrack = new List<(int x, int y, char dir, int count, int heat)>();
            var child = solution.Key;
            backtrack.Add(child);
            var test = backTracking.Where(x => x.Key.count == 3).Count();
            while (backTracking.TryGetValue(child, out var parent))
            {
                backtrack.Add(parent);
                child = parent;
            }
            yield return updateContext();
            provideSolution(bestArrivalHeat.ToString());
        }
    }
}
