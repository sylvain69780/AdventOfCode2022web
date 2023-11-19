using System.Text.RegularExpressions;

namespace Domain.ProboscideaVolcanium
{
    public class ProboscideaVolcaniumSolution : IPuzzleSolution
    {
        private string _puzzleInput = string.Empty;
        private const string StartingValve = "AA";
        private const int MinutesAllowedFirstPart = 30;
        private const int MinutesAllowedSecondPart = 26;

        public IEnumerable<string> SolveFirstPart()
        {
            var valves = GetValves(_puzzleInput);
            var valvesToVisit = valves.Values.Where(x => x.Rate > 0).Select(x => x.Name).ToArray();
            var distancesBetweenValves = ComputeDistanceBetweenAllValves(valves, valvesToVisit);
            var (optimalPressureReleased, optimalFlow) = (0, "");
            foreach (var (pressureReleased, flow) in ComputeOptimalFlow(valves, distancesBetweenValves, valvesToVisit, MinutesAllowedFirstPart))
            {
                if (pressureReleased > optimalPressureReleased)
                    (optimalPressureReleased, optimalFlow) = (pressureReleased, flow);
            }
            yield return optimalFlow + '\n' + optimalPressureReleased.ToString();
        }
        public IEnumerable<string> SolveSecondPart()
        {
            var valves = GetValves(_puzzleInput);
            var valvesToVisit = valves.Values.Where(x => x.Rate > 0).Select(x => x.Name).ToArray();
            var distancesBetweenValves = ComputeDistanceBetweenAllValves(valves, valvesToVisit);

            var (optimalPressureReleasedSingle, optimalFlowSingle) = (0, string.Empty);
            foreach (var (pressureReleased, flow) in ComputeOptimalFlow(valves, distancesBetweenValves, valvesToVisit, MinutesAllowedSecondPart))
            {
                if (pressureReleased > optimalPressureReleasedSingle)
                    (optimalPressureReleasedSingle, optimalFlowSingle) = (pressureReleased, flow);
            }

            var (optimalPressureReleasedRemaining, optimalFlowRemaining) = (0, string.Empty);
            {
                var split = optimalFlowSingle.Split(',');
                var valvesToVisitRemaining = valvesToVisit.Where(x => Array.IndexOf(split, x) == -1).ToArray();
                foreach (var (pressureReleased, flow) in ComputeOptimalFlow(valves, distancesBetweenValves, valvesToVisitRemaining, MinutesAllowedSecondPart))
                {
                    if (pressureReleased > optimalPressureReleasedRemaining)
                        (optimalPressureReleasedRemaining, optimalFlowRemaining) = (pressureReleased, flow);
                }
            }
            // https://jactl.io/blog/2023/04/21/advent-of-code-2022-day16.html
            var (optimalPressureReleasedPrimary, optimalFlowPrimary) = (0, string.Empty);
            var (optimalPressureReleasedSecondary, optimalFlowSecondary) = (0, string.Empty);
            var optimalPressureReleasedCombined = optimalPressureReleasedSingle;
            foreach (var (pressureReleasedPrimary, flowPrimary) in ComputeOptimalFlow(valves, distancesBetweenValves, valvesToVisit, MinutesAllowedSecondPart))
            {
                if (pressureReleasedPrimary > optimalPressureReleasedRemaining)
                {
                    var split = flowPrimary.Split(',');
                    var valvesToVisitSecondary = valvesToVisit.Where(x => Array.IndexOf(split, x) == -1).ToArray();
                    foreach (var (pressureReleasedSecondary, flowSecondary) in ComputeOptimalFlow(valves, distancesBetweenValves, valvesToVisitSecondary, MinutesAllowedSecondPart))
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
            yield return optimalFlowPrimary + "\n" + optimalPressureReleasedPrimary.ToString() + "\n"
                + optimalFlowSecondary + "\n" + optimalPressureReleasedSecondary.ToString() + "\n"
                + optimalPressureReleasedCombined.ToString();
        }

        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }
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
            void addToDistances(string firstValve, string secondValve) 
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

        private static IEnumerable<(int BestPressure, string OptimalFlow)> ComputeOptimalFlow(Dictionary<string, Valve> valves, Dictionary<(string a, string b), int> distancesBetweenValves, string[] valvesToVisit, int minutesAllowed)
        {
            var queue = new Queue<string>();
            queue.Enqueue(StartingValve);
            while (queue.Count > 0)
            {
                var newQueue = new Queue<string>();
                while (queue.TryDequeue(out var currentFlowString))
                {
                    var currentFlow = currentFlowString.Split(',');
                    var (timeElapsed, pressureReleased) = ComputeTimeElapsedAndPressureReleased(currentFlow, valves, distancesBetweenValves, minutesAllowed);
                    var minutesRemaining = minutesAllowed - timeElapsed - 2;
                    var currentValve = currentFlow[^1];
                    var valvesRemainingToVisit = valvesToVisit
                        .Where(x => Array.IndexOf(currentFlow, x) == -1 && distancesBetweenValves[(currentValve, x)] < minutesRemaining).ToArray();
                    yield return (pressureReleased, currentFlowString);
                    foreach (var valve in valvesRemainingToVisit)
                        newQueue.Enqueue(currentFlowString + ',' + valve);
                }
                queue = newQueue;
            }
        }
    }
}
