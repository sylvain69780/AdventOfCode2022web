using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NotEnoughMinerals
{
    public class NotEnoughMineralsPart2Strategy : IPuzzleStrategy<NotEnoughMineralsModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(NotEnoughMineralsModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var quality = 1;
            var maxMinutes = 32;
            foreach (var bp in model.BluePrints!.Take(3))
            {
                var (maxGeodes, iterationsDone) = NotEnoughMineralsModel.MaxGeodesPossible(bp, maxMinutes);
                yield return updateContext();
                // $"Blueprint {bp.BlueprintNumber} gives at most {maxGeodes} geodes. {iterationsDone} iterations done.";
                quality *= maxGeodes;
            }
            yield return updateContext();
            provideSolution($"{quality}");
        }
    }
}
