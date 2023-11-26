using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CathodeRayTube
{
    public class CathodeRayTubePart1Strategy : IPuzzleStrategy<CathodeRayTubeModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(CathodeRayTubeModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var valueOfXregister = 1;
            var cycleToRecord = 20;
            var currentCycle = 0;
            var sumOfSixSignalStrengths = 0;
            foreach (var value in model.NumsToAdd!)
            {
                currentCycle++;
                if (currentCycle == cycleToRecord)
                {
                    cycleToRecord += 40;
                    sumOfSixSignalStrengths += valueOfXregister * currentCycle;
                }
                valueOfXregister += value;
            }
            yield return updateContext();
            provideSolution(sumOfSixSignalStrengths.ToString());
        }
    }
}
