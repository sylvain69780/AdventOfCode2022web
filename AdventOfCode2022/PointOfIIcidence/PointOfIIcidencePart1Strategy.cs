using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PointOfIIcidence
{
    public class PointOfIIcidencePart1Strategy : IPuzzleStrategy<PointOfIIcidenceModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(PointOfIIcidenceModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var patterns = model.Patterns!;
            var sum = 0L;
            foreach (var pattern in patterns)
            {
                sum += ReflextedColumn(pattern);
                sum += 100*ReflextedRows(pattern);
            }

            yield return updateContext();
            provideSolution(sum.ToString());
        }

        private static long ReflextedColumn((int width, int height, string[] map) pattern)
        {
            var map = pattern.map;
            for (var m = 1; m < pattern.width; m++)
            {
                var sb1 = new StringBuilder();
                var sb2 = new StringBuilder();
                var reflected = true;
                for (var x = 0; m + x < pattern.width && m - x - 1 >=0; x++)
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
                    return m;
            }
            return 0;
        }

        private static long ReflextedRows((int width, int height, string[] map) pattern)
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
                    return m;
            }
            return 0;
        }

    }
}
