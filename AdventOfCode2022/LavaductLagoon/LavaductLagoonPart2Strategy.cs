using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LavaductLagoon
{
    public class LavaductLagoonPart2Strategy : IPuzzleStrategy<LavaductLagoonModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(LavaductLagoonModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var digplan = model.DigPlan!;
            var path = new List<(long x, long y)>();
            var pos = (x: 0L, y: 0L);
            path.Add(pos);
            foreach (var dig in digplan)
            {
                var amount = long.Parse(dig.rgb.Substring(2, 5), System.Globalization.NumberStyles.HexNumber);
                var dir = dig.rgb[7];
                var dir2 = dir switch
                {
                    '0' => 'R',
                    '1' => 'D',
                    '2' => 'L',
                    '3' => 'U'
                };
                var (x, y) = dir2 switch
                {
                    'R' => (x: 1, y: 0),
                    'L' => (x: -1, y: 0),
                    'D' => (x: 0, y: 1),
                    'U' => (x: 0, y: -1),
                    _ => throw new NotImplementedException(),
                };
                pos = (pos.x + x * amount, pos.y + y * amount);
                path.Add(pos);
            }
            var count = 0L;

            var gridX = path.Select(p => p.x).SelectMany(x => new long[] { x, x + 1L }).Distinct().OrderBy(x => x).ToArray();
            var gridY = path.Select(p => p.y).SelectMany(y => new long[] { y, y + 1L }).Distinct().OrderBy(y => y).ToArray();

            var grid2 = new char[gridX.Length, gridY.Length];
            for (var y = 0; y < grid2.GetLength(1); y++)
                for (var x = 0; x < grid2.GetLength(0); x++)
                    grid2[x, y] = ' ';

            var mapping = path.Skip(1).Select(p => (p, p2: (x: Array.IndexOf(gridX, p.x), y: Array.IndexOf(gridY, p.y)))).ToDictionary(x => x.p, x => x.p2);
            for (var i = 1; i < path.Count; i++)
            {
                var p1 = mapping[path[i - 1]];
                var p2 = mapping[path[i]];
                if (p2.x > p1.x)
                    for (var j = p1.x + 1; j <= p2.x; j++)
                        grid2[j, p1.y] = '>';
                if (p2.x < p1.x)
                    for (var j = p1.x - 1; j >= p2.x; j--)
                        grid2[j, p1.y] = '<';
                if (p2.y > p1.y)
                    for (var j = p1.y; j <= p2.y; j++)
                        grid2[p1.x, j] = 'v';
                if (p2.y < p1.y)
                    for (var j = p1.y; j >= p2.y; j--)
                        grid2[p1.x, j] = '^';
            }
            // fill
            for (var y = 0; y < grid2.GetLength(1) - 1; y++)
            {
                var onborder = false;
                var inside = false;
                var lastValue = ' ';
                for (var x = 0; x < grid2.GetLength(0) - 1; x++)
                {
                    var value = grid2[x, y];
                    if (value == ' ')
                    {
                        lastValue = ' ';
                        onborder = false;
                    }
                    else
                    {
                        onborder = true;
                        if (value == '^' || value == 'v')
                        {
                            if (value != lastValue)
                                inside = !inside;
                            lastValue = value;
                        }
                    }
                    if (inside || onborder)
                    {
                        count += (gridX[x + 1] - gridX[x]) * (gridY[y + 1] - gridY[y]);
                        if (value == ' ')
                            grid2[x, y] = '#';
                    }
                }
            }
            var sb = new StringBuilder();
            for (var y = 0; y < grid2.GetLength(1) - 1; y++)
            {
                for (var x = 0; x < grid2.GetLength(0) - 1; x++)
                    sb.Append(grid2[x, y]);
                sb.Append('\n');
            }
            var o = sb.ToString();

            yield return updateContext();
            provideSolution(count.ToString());
        }
    }
}
