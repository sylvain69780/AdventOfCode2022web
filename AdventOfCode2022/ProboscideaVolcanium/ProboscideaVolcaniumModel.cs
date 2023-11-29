using System.Text.RegularExpressions;

namespace Domain.ProboscideaVolcanium
{
    public class ProboscideaVolcaniumModel : IPuzzleModel
    {
        public const string StartingValve  = "AA";
        public Dictionary<string, Valve>? Valves { get; set; }
        public void Parse(string input)
        {
            Valves = GetValves(input);
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

        public Dictionary<(string a, string b), int> ComputeDistanceBetweenAllValves(string[] valvesToVisit)
        {
            var distancesBetweenValves = new Dictionary<(string a, string b), int>();
            void addToDistances(string firstValve, string secondValve)
            {
                var d = ComputeDistanceBetweenTwoValves(firstValve, secondValve);
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

        private int ComputeDistanceBetweenTwoValves(string firstValve, string secondValve)
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
                    var nextValves = Valves![currentValve].LeadsToValves;
                    if (Array.IndexOf(nextValves, secondValve) != -1)
                        return distance;
                    foreach (var valve in nextValves)
                        newQueue.Enqueue(valve);
                }
                queue = newQueue;
            }
            return distance;
        }

        private  (int, int) ComputeTimeElapsedAndPressureReleased(string[] flow, Dictionary<(string a, string b), int> distancesBetweenValves, int minutesAllowed)
        {
            var (timeElapsed, pressureReleased) = (0, 0);
            for (var i = 0; i < flow.Length - 1; i++)
            {
                timeElapsed += distancesBetweenValves[(flow[i], flow[i + 1])] + 1;
                pressureReleased += (minutesAllowed - timeElapsed) * Valves![flow[i + 1]].Rate;
            }
            return (timeElapsed, pressureReleased);
        }

        public IEnumerable<(int BestPressure, string OptimalFlow)> ComputeOptimalFlow(Dictionary<(string a, string b), int> distancesBetweenValves, string[] valvesToVisit, int minutesAllowed)
        {
            var queue = new Queue<string>();
            queue.Enqueue(StartingValve);
            while (queue.Count > 0)
            {
                var newQueue = new Queue<string>();
                while (queue.TryDequeue(out var currentFlowString))
                {
                    var currentFlow = currentFlowString.Split(',');
                    var (timeElapsed, pressureReleased) = ComputeTimeElapsedAndPressureReleased(currentFlow,distancesBetweenValves, minutesAllowed);
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
