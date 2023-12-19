using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LavaductLagoon
{
    public class LavaductLagoonPart1Strategy : IPuzzleStrategy<LavaductLagoonModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(LavaductLagoonModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var digplan = model.DigPlan!;
            var path = new List<(int x, int y)>();
            var pos = (x: 0, y: 0);
            path.Add(pos);
            foreach (var dig in digplan)
            {
                var v = dig.dir switch
                {
                    'R' => (x: 1, y: 0),
                    'L' => (x: -1, y: 0),
                    'D' => (x: 0, y: 1),
                    'U' => (x: 0, y: -1),
                    _ => throw new NotImplementedException(),
                };
                //for (var i = 0; i < dig.count; i++)
                //{
                pos = (pos.x + v.x * dig.count, pos.y + v.y * dig.count);
                path.Add(pos);
            }
            var box = (left: int.MaxValue, top: int.MaxValue, right: int.MinValue, bottom: int.MinValue);
            box = path.Aggregate(box, (box, p) => (
                left: Math.Min(box.left, p.x),
                top: Math.Min(box.top, p.y),
                right: Math.Max(box.right, p.x),
                bottom: Math.Max(box.bottom, p.y)
            ));
//            var grid = new char[box.right - box.left + 1, box.bottom - box.top + 1];

            var count = 0;

            var gridX = path.Select(p => p.x).SelectMany(x => new int[] { x, x + 1 }).Distinct().OrderBy(x => x).ToArray();
            var gridY = path.Select(p => p.y).SelectMany(y => new int[] { y, y + 1 }).Distinct().OrderBy(y => y).ToArray();

            //// integrate per part
            //for (var y = 0; y<gridY.Length;y++)
            //    for (var x = 0; x < gridY.Length; x++)
            //    {
            //        var s = (gridX[x + 1] - gridX[x]) * (gridY[y + 1] - gridY[y]);
            //        var a = (x: gridX[x], y: gridY[y]);
            //        for (var i = 1; i < path.Count; i++)
            //        {
            //            var p1 = path[i - 1];
            //            var p2 = path[i];

            //        }
            //    }


            var grid2 = new char[gridX.Length, gridY.Length];
            for (var y = 0; y < grid2.GetLength(1); y++)
                for (var x = 0; x < grid2.GetLength(0); x++)
                    grid2[x, y] = ' ';

            var mapping = path.Skip(1).Select(p => (p, p2: (x: Array.IndexOf(gridX, p.x), y: Array.IndexOf(gridY, p.y)))).ToDictionary(x => x.p, x => x.p2);
            for (var i = 1; i < path.Count; i++)
            {
                var p1 = mapping[path[i - 1]];
                var p2 = mapping[path[i]];
//                grid2[p1.x, p1.y] = 1;
                if (p2.x > p1.x)
                    for (var j = p1.x+1; j <= p2.x; j++)
                        grid2[j, p1.y] = '>';
                if (p2.x < p1.x)
                    for (var j = p1.x-1; j >= p2.x; j--)
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
