using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProboscideaVolcanium
{
    public class ProboscideaVolcaniumPart2Strategy : IPuzzleStrategy<ProboscideaVolcaniumModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(ProboscideaVolcaniumModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var valvesToVisit = model.Valves!.Values.Where(x => x.Rate > 0).Select(x => x.Name).ToArray();
            var distancesBetweenValves = model.ComputeDistanceBetweenAllValves(valvesToVisit);

            var (optimalPressureReleasedSingle, optimalFlowSingle) = (0, string.Empty);
            foreach (var (pressureReleased, flow) in model.ComputeOptimalFlow(distancesBetweenValves, valvesToVisit, MinutesAllowedSecondPart))
            {
                if (pressureReleased > optimalPressureReleasedSingle)
                    (optimalPressureReleasedSingle, optimalFlowSingle) = (pressureReleased, flow);
            }

            var (optimalPressureReleasedRemaining, optimalFlowRemaining) = (0, string.Empty);
            {
                var split = optimalFlowSingle.Split(',');
                var valvesToVisitRemaining = valvesToVisit.Where(x => Array.IndexOf(split, x) == -1).ToArray();
                foreach (var (pressureReleased, flow) in model.ComputeOptimalFlow(distancesBetweenValves, valvesToVisitRemaining, MinutesAllowedSecondPart))
                {
                    if (pressureReleased > optimalPressureReleasedRemaining)
                        (optimalPressureReleasedRemaining, optimalFlowRemaining) = (pressureReleased, flow);
                }
            }
            // https://jactl.io/blog/2023/04/21/advent-of-code-2022-day16.html
            var (optimalPressureReleasedPrimary, optimalFlowPrimary) = (0, string.Empty);
            var (optimalPressureReleasedSecondary, optimalFlowSecondary) = (0, string.Empty);
            var optimalPressureReleasedCombined = optimalPressureReleasedSingle;
            foreach (var (pressureReleasedPrimary, flowPrimary) in model.ComputeOptimalFlow(distancesBetweenValves, valvesToVisit, MinutesAllowedSecondPart))
            {
                if (pressureReleasedPrimary > optimalPressureReleasedRemaining)
                {
                    var split = flowPrimary.Split(',');
                    var valvesToVisitSecondary = valvesToVisit.Where(x => Array.IndexOf(split, x) == -1).ToArray();
                    foreach (var (pressureReleasedSecondary, flowSecondary) in model.ComputeOptimalFlow(distancesBetweenValves, valvesToVisitSecondary, MinutesAllowedSecondPart))
                    {
                        if (pressureReleasedPrimary + pressureReleasedSecondary > optimalPressureReleasedCombined)
                        {
                            optimalPressureReleasedCombined = pressureReleasedPrimary + pressureReleasedSecondary;
                            (optimalPressureReleasedPrimary, optimalFlowPrimary) = (pressureReleasedPrimary, flowPrimary);
                            (optimalPressureReleasedSecondary, optimalFlowSecondary) = (pressureReleasedSecondary, flowSecondary);
                        }
                    }
                }
            }
            yield return updateContext();
            //provideSolution(optimalFlowPrimary + "\n" + optimalPressureReleasedPrimary.ToString() + "\n"
            //    + optimalFlowSecondary + "\n" + optimalPressureReleasedSecondary.ToString() + "\n"
            //    + optimalPressureReleasedCombined.ToString());
            provideSolution(optimalPressureReleasedCombined.ToString());
        }
        private const int MinutesAllowedSecondPart = 26;

    }
}
