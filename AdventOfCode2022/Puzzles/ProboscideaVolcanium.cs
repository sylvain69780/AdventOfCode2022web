using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(16, "Proboscidea Volcanium")]
    public class ProboscideaVolcanium : IPuzzleSolver
    {
        struct Valve
        {
            public string Name;
            public int Rate;
            public string[] LeadsToValves;
        }

        private static Dictionary<string, Valve> GetValves(string puzzleInput)
        {
            var regex = new Regex(@"Valve (..) has flow rate=(\d+); tunnels? leads? to valves? (.+)", RegexOptions.Compiled);
            return puzzleInput.Split("\n")
                .Select(x => regex.Matches(x))
                .Select(x => new Valve
                {
                    Name = x[0].Groups[1].Value,
                    Rate = int.Parse(x[0].Groups[2].Value),
                    LeadsToValves = x[0].Groups[3].Value.Replace(", ", ",").Split(',')
                })
                .ToDictionary(x => x.Name);
        }

        private static Dictionary<(string a, string b), int> ComputeDistanceBetweenAllValves(Dictionary<string, Valve> valves, string[] valvesToVisit)
        {
            var distancesBetweenValves = new Dictionary<(string a, string b), int>();
            var addToDistances = (string firstValve, string secondValve) =>
            {
                var d = ComputeDistanceBetweenTwoValves(valves, firstValve, secondValve);
                distancesBetweenValves.Add((firstValve, secondValve), d);
                distancesBetweenValves.Add((secondValve, firstValve), d);
            };
            for (var i = 0; i < valvesToVisit.Length; i++)
                addToDistances(valvesToVisit[i], StartingValve);
            for (var i = 0; i < valvesToVisit.Length - 1; i++)
                for (var j = i + 1; j < valvesToVisit.Length; j++)
                    addToDistances(valvesToVisit[i], valvesToVisit[j]);
            return distancesBetweenValves;
        }

        private static int ComputeDistanceBetweenTwoValves(Dictionary<string, Valve> valves, string firstValve, string secondValve)
        {
            var queue = new Queue<string>();
            var distance = 0;
            queue.Enqueue(firstValve);
            while (queue.Count > 0)
            {
                var newQueue = new Queue<string>();
                distance++;
                while (queue.TryDequeue(out var currentValve))
                {
                    var nextValves = valves[currentValve].LeadsToValves;
                    if (Array.IndexOf(nextValves, secondValve) != -1)
                        return distance;
                    foreach (var valve in nextValves)
                        newQueue.Enqueue(valve);
                }
                queue = newQueue;
            }
            return distance;
        }

        private static (int, int) ComputeTimeElapsedAndPressureReleased(string[] flow, Dictionary<string, Valve> valves, Dictionary<(string a, string b), int> distancesBetweenValves, int minutesAllowed)
        {
            var (timeElapsed, pressureReleased) = (0, 0);
            for (var i = 0; i < flow.Length - 1; i++)
            {
                timeElapsed += distancesBetweenValves[(flow[i], flow[i + 1])] + 1;
                pressureReleased += (minutesAllowed - timeElapsed) * valves[flow[i + 1]].Rate;
            }
            return (timeElapsed, pressureReleased);
        }

        private static (int BestPressure, string OptimalFlow) ComputeOptimalFlow(Dictionary<string, Valve> valves, Dictionary<(string a, string b), int> distancesBetweenValves, string[] valvesToVisit, int minutesAllowed, int minPressureReleaseExpected)
        {
            var queue = new Queue<string>();
            var (bestPressureReleased, bestFlow) = (minPressureReleaseExpected, "");
            queue.Enqueue(StartingValve);
            while (queue.Count > 0)
            {
                var newQueue = new Queue<string>();
                while (queue.TryDequeue(out var currentFlowString))
                {
                    var currentFlow = currentFlowString.Split(',');
                    var (timeElapsed, pressureReleased) = ComputeTimeElapsedAndPressureReleased(currentFlow, valves, distancesBetweenValves, minutesAllowed);
                    var minutesRemaining = minutesAllowed - timeElapsed - 2;
                    var valvesRemainingToVisit = valvesToVisit.Where(x => Array.IndexOf(currentFlow, x) == -1);
                    var maxPressurePossibleToRelease = valvesRemainingToVisit.Select(x => valves[x])
                        .Sum(x => x.Rate * minutesRemaining);
                    if (pressureReleased + maxPressurePossibleToRelease < bestPressureReleased)
                        continue;
                    var currentValve = currentFlow[^1];
                    if (!valvesRemainingToVisit.Any(x => distancesBetweenValves[(currentValve, x)] < minutesRemaining))
                    {
                        if (bestPressureReleased <= pressureReleased)
                        {
                            bestPressureReleased = pressureReleased;
                            bestFlow = currentFlowString;
                        }
                    }
                    foreach (var valve in valvesRemainingToVisit)
                        newQueue.Enqueue(currentFlowString + ',' + valve);
                }
                queue = newQueue;
            }
            return (bestPressureReleased, bestFlow);
        }

        private static (int OptimalPressureReleased, string OptimalPrimaryFlow, string OptimalSecondaryFlow) ComputeOptimalPrimaryAndSecondaryFlows(Dictionary<string, Valve> valves, Dictionary<(string a, string b), int> distancesBetweenValves, string[] valvesToVisit, int minutesAllowed)
        {
            var queue = new Queue<string>();
            var (optimalPressureReleased, optimalFlow) = ComputeOptimalFlow(valves, distancesBetweenValves, valvesToVisit, minutesAllowed, 0);
            var optimalFlowSplit = optimalFlow.Split(',');
            var valvesToVisitRemaining = valvesToVisit.Where(x => Array.IndexOf(optimalFlowSplit, x) == -1).ToArray();
            var (minPressureReleaseExpected, optimalFlowRemaining) = ComputeOptimalFlow(valves, distancesBetweenValves, valvesToVisitRemaining, minutesAllowed, 0);
            var (bestPressureReleased, bestPrimaryFlow) = (optimalPressureReleased, "");
            var (bestPressureReleasedSecondary, bestSecondaryFlow) = (0, "");
            queue.Enqueue(StartingValve + ',' + optimalFlowSplit[1]);
            while (queue.Count > 0)
            {
                var newQueue = new Queue<string>();
                while (queue.TryDequeue(out var CurrentPrimaryFlowString))
                {
                    var currentPrimaryFlow = CurrentPrimaryFlowString.Split(',');
                    var valvesRemainingToVisit = valvesToVisit
                        .Where(x => Array.IndexOf(currentPrimaryFlow, x) == -1).ToArray();
                    var (timeElapsedPrimary, pressureReleasedPrimary) = ComputeTimeElapsedAndPressureReleased(currentPrimaryFlow, valves, distancesBetweenValves, minutesAllowed);
                    var minutesRemainingPrimary = minutesAllowed - timeElapsedPrimary - 2;
                    var valvesRemainingToVisitPrimary = valvesRemainingToVisit
                        .Where(x => distancesBetweenValves[(currentPrimaryFlow[^1], x)] <= minutesRemainingPrimary)
                        .ToArray();
                    if (pressureReleasedPrimary > minPressureReleaseExpected)
                    {
                        var (pressureReleasedSecondary, secondaryFlow) = ComputeOptimalFlow(valves, distancesBetweenValves, valvesRemainingToVisit, minutesAllowed, minPressureReleaseExpected);
                        if (pressureReleasedSecondary < pressureReleasedPrimary 
                            && pressureReleasedSecondary >= minPressureReleaseExpected
                            && pressureReleasedPrimary + pressureReleasedSecondary >= bestPressureReleased)
                        {
                            bestSecondaryFlow = secondaryFlow;
                            bestPressureReleasedSecondary = pressureReleasedSecondary;
                            bestPrimaryFlow = CurrentPrimaryFlowString;
                            bestPressureReleased = pressureReleasedPrimary + pressureReleasedSecondary;
                        }
                    }
                    foreach (var valve in valvesRemainingToVisitPrimary)
                        newQueue.Enqueue(CurrentPrimaryFlowString + ',' + valve);
                }
                queue = newQueue;
                Console.WriteLine(queue.Count.ToString());
            }
            return (bestPressureReleased, bestPrimaryFlow, bestSecondaryFlow);
        }

        private const string StartingValve = "AA";
        private const int MinutesAllowedFirstPart = 30;
        private const int MinutesAllowedSecondPart = 26;

        public string SolveFirstPart(string puzzleInput)
        {
            var valves = GetValves(puzzleInput);
            var valvesToVisit = valves.Values.Where(x => x.Rate > 0).Select(x => x.Name).ToArray();
            var distancesBetweenValves = ComputeDistanceBetweenAllValves(valves, valvesToVisit);
            var (optimalPressureReleased, optimalFlow) = ComputeOptimalFlow(valves, distancesBetweenValves, valvesToVisit, MinutesAllowedFirstPart, 0);
            return optimalFlow + '\n' + optimalPressureReleased.ToString();
        }
        public string SolveSecondPart(string puzzleInput)
        {
            var valves = GetValves(puzzleInput);
            var valvesToVisit = valves.Values.Where(x => x.Rate > 0).Select(x => x.Name).ToArray();
            var distancesBetweenValves = ComputeDistanceBetweenAllValves(valves, valvesToVisit);
            var (optimalPressureReleased, optimalPrimaryFlow, optimalSecondaryFlow) = ComputeOptimalPrimaryAndSecondaryFlows(valves, distancesBetweenValves, valvesToVisit, MinutesAllowedSecondPart);
            return optimalPrimaryFlow + '\n' + optimalSecondaryFlow + '\n' + optimalPressureReleased.ToString();
        }
    }
}
