using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.GearRatios
{
    public class GearRatiosPart1Strategy : IPuzzleStrategy<GearRatiosModel>
    {
        public string Name { get; set; } = "Part 1";


        public IEnumerable<ProcessingProgressModel> GetSteps(GearRatiosModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var sum = 0;
            foreach (var (x, y, val) in model.PartNumbers)
            {
                if (model.Symbols.Any(s => GearRatiosModel.Distance((x, y, val.Length), (s.x, s.y)) == 1))
                    sum += int.Parse(val);
            }
            yield return updateContext();
            provideSolution(sum.ToString());
        }
    }
}
