using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NotEnoughMinerals
{
    public class NotEnoughMineralsPart1Strategy : IPuzzleStrategy<NotEnoughMineralsModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(NotEnoughMineralsModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var quality = 0;
            var maxMinutes = 24;
            foreach (var bp in model.BluePrints!)
            {
                var (maxGeodes, iterationsDone) = NotEnoughMineralsModel.MaxGeodesPossible(bp, maxMinutes);
                yield return updateContext();
                // $"Blueprint {bp.BlueprintNumber} gives at most {maxGeodes} geodes. {iterationsDone} iterations done.";
                quality += maxGeodes * bp.BlueprintNumber;
            }
            yield return updateContext();
            provideSolution($"{quality}");
        }
    }
}
