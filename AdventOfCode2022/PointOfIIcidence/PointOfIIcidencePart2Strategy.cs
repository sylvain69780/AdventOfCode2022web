using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PointOfIIcidence
{
    public class PointOfIIcidencePart2Strategy : IPuzzleStrategy<PointOfIIcidenceModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(PointOfIIcidenceModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var patterns = model.Patterns!;
            var sum = 0L;
            foreach (var pattern in patterns)
            {
                var r1 = ReflextedColumn(pattern).SingleOrDefault();
                var r2 = ReflextedRows(pattern).SingleOrDefault();
                var newr1 = new List<long>();
                var newr2 = new List<long>();
                var map = pattern.map;
                var found = false;
                foreach ( var newMap in Enumerable
                        .Range(0, pattern.width)
                        .SelectMany(x => Enumerable.Range(0, pattern.height).Select(y => (x, y)))
                        .Select(p => map[p.y][p.x] == '.' ? (p.x, p.y, c: '#') : (p.x, p.y, c: '.'))
                        .Select(p => (p.x,p.y,map:map.Select((l, i) => i == p.y ? l.Substring(0, p.x) + p.c + l.Substring(p.x+1) : l).ToArray()))
                        )
                {
                    var newPattern = (pattern.width, pattern.height, newMap.map);
                    var tmp1 = ReflextedColumn(newPattern).Where(x => x!=r1).SingleOrDefault();
                    if (tmp1 != 0)
                    {
                        newr1.Add(tmp1);
                        r1 = 0;
                        r2 = 0;
                        found = true;
                        break;
                    }
                    var tmp2 = ReflextedRows(newPattern).Where(x => x != r2).SingleOrDefault();
                    if (tmp2 != 0)
                    {
                        newr2.Add(tmp2);
                        r1 = 0;
                        r2 = 0;
                        found = true;
                        break;
                    }
                }
                if (!found)
                    Console.WriteLine("");
                var rr1 = newr1.Distinct().SingleOrDefault();
                var rr2 = newr2.Distinct().SingleOrDefault();
//                r1 = r2 = 0;
                sum += r1+rr1;
                sum += 100*(r2+rr2);
            }

            yield return updateContext();
            provideSolution(sum.ToString());
        }

        private static IEnumerable<long> ReflextedColumn((int width, int height, string[] map) pattern)
        {
            var map = pattern.map;
            for (var m = 1; m < pattern.width; m++)
            {
                var sb1 = new StringBuilder();
                var sb2 = new StringBuilder();
                var reflected = true;
                for (var x = 0; m + x < pattern.width && m - x - 1 >= 0; x++)
                {
                    var i1 = m - x - 1;
                    var i2 = m + x;
                    for (var y = 0; y < pattern.height; y++)
                    {
                        sb1.Append(map[y][i1]);
                        sb2.Append(map[y][i2]);
                    }
                    if (sb1.ToString() != sb2.ToString())
                    {
                        reflected = false;
                        break;
                    }
                }
                if (reflected)
                    yield return m;
            }
        }

        private static IEnumerable<long> ReflextedRows((int width, int height, string[] map) pattern)
        {
            var map = pattern.map;
            for (var m = 1; m < pattern.height; m++)
            {
                var reflected = true;
                for (var y = 0; m + y < pattern.height && m - y - 1 >= 0; y++)
                {
                    var i1 = m - y - 1;
                    var i2 = m + y;
                    if (map[i1] != map[i2])
                    {
                        reflected = false;
                        break;
                    }
                }
                if (reflected)
                    yield return m;
            }
        }
    }
}
