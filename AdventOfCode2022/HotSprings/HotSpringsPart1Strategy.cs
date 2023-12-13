using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HotSprings
{
    public class HotSpringsPart1Strategy : IPuzzleStrategy<HotSpringsModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(HotSpringsModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var rows = model.Rows!;
            var sum = rows.Select(row =>row.springs.PossibleConfigurationsOptim(row.ranges)).Sum();

            yield return updateContext();
            provideSolution(sum.ToString());
        }

        // first version
        public IEnumerable<ProcessingProgressModel> GetStepsV0(HotSpringsModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var rows = model.Rows!;
            var sum = rows.Select(row => row.springs.PossibleConfigurations()
                .Select(x => (springs: x, groups: x.Groups().ToArray()))
                .Where(x => x.groups.Length == row.ranges.Length && !Enumerable.Range(0, x.groups.Length).Any(i => x.groups[i] != row.ranges[i]))
                .Count()).Sum();

            yield return updateContext();
            provideSolution(sum.ToString());
        }
    }
}
