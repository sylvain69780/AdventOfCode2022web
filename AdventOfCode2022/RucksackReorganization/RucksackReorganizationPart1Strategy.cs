using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RucksackReorganization
{
    public class RucksackReorganizationPart1Strategy : RucksackReorganizationStrategyBase
    {
        public override string Name { get; set; } = "Part 1";

        public override IEnumerable<ProcessingProgressModel> GetSteps(RucksackReorganizationModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var score = 0;
            foreach (var rucksack in model.Lines!)
            {
                var compartmentSize = rucksack.Length / 2;
                var (compartmentA, compartmentB) = (rucksack[..compartmentSize], rucksack[compartmentSize..(compartmentSize + compartmentSize)]);
                var sharedItem = compartmentA.First(x => compartmentB.Contains(x));
                score += Priority(sharedItem);
                yield return updateContext();
            }
            provideSolution(score.ToString());
        }
    }
}
