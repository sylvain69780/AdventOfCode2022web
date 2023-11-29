using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProboscideaVolcanium
{
    public class ProboscideaVolcaniumPart1Strategy : IPuzzleStrategy<ProboscideaVolcaniumModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(ProboscideaVolcaniumModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var valvesToVisit = model.Valves!.Values.Where(x => x.Rate > 0).Select(x => x.Name).ToArray();
            var distancesBetweenValves = model.ComputeDistanceBetweenAllValves(valvesToVisit);
            var (optimalPressureReleased, optimalFlow) = (0, "");
            foreach (var (pressureReleased, flow) in model.ComputeOptimalFlow(distancesBetweenValves, valvesToVisit, MinutesAllowedFirstPart))
            {
                if (pressureReleased > optimalPressureReleased)
                    (optimalPressureReleased, optimalFlow) = (pressureReleased, flow);
            }
            yield return updateContext();
            // provideSolution(optimalFlow + '\n' + optimalPressureReleased.ToString());
            provideSolution(optimalPressureReleased.ToString());
        }

        private const int MinutesAllowedFirstPart = 30;

    }
}
