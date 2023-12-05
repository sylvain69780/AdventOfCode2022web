using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.GearRatios
{
    public class GearRatiosPart2Strategy : IPuzzleStrategy<GearRatiosModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(GearRatiosModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var sum = 0;
            foreach (var (x, y, val) in model.Symbols)
            {
                if (val != '*')
                    continue;
                var parts = model.PartNumbers
                    .Where(s => GearRatiosModel.Distance((s.x, s.y, s.val.Length), (x, y)) == 1)
                    .Select(s => int.Parse(s.val))
                    .ToArray();
                if (parts.Length == 2)
                    sum += parts[0]*parts[1];
            }
            yield return updateContext();
            provideSolution(sum.ToString());
        }
    }
}
