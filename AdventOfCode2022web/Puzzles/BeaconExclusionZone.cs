using System.Text.RegularExpressions;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(15, "Beacon Exclusion Zone")]
    public class BeaconExclusionZone : IPuzzleSolver
    {
        private class SensorPositionAndClosestBeacon
        {
            public (int x, int y) Sensor;
            public (int x, int y) Beacon;
            public int ManhattanDistance;
        }

        private static int ManhattanDistance((int x, int y) a, (int x, int y) b) => Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);

        private List<SensorPositionAndClosestBeacon> GetSensorPositionAndClosestBeacons(string puzzleInput)
        {
            var coordinatesRegex = new Regex(@"x=(-?\d+), y=(-?\d+)", RegexOptions.Compiled);
            return puzzleInput.Split("\n")
                .Select(x => coordinatesRegex.Matches(x))
                .Select(x => new
                {
                    Sensor = (x: int.Parse(x[0].Groups[1].Value), y: int.Parse(x[0].Groups[2].Value)),
                    Beacon = (x: int.Parse(x[1].Groups[1].Value), y: int.Parse(x[1].Groups[2].Value)),
                })
                .Select(x => new SensorPositionAndClosestBeacon
                {
                    Sensor = x.Sensor,
                    Beacon = x.Beacon,
                    ManhattanDistance = ManhattanDistance(x.Sensor, x.Beacon)
                })
                .ToList();
        }

        public string SolveFirstPart(string puzzleInput)
        {
            var sensorsPositionsAndClosestBeacon = GetSensorPositionAndClosestBeacons(puzzleInput);
            var verticalPositionOfRowToAnalyze = sensorsPositionsAndClosestBeacon.Count <= 14 ? 10 : 2000000; 
            var horizontalIntervalsOnRowToAnalyze = new List<(int begin, int end)>();
            foreach (var record in sensorsPositionsAndClosestBeacon)
            {
                var distanceOfSensorToRowToAnalyze = Math.Abs(record.Sensor.y - verticalPositionOfRowToAnalyze);
                if (distanceOfSensorToRowToAnalyze <= record.ManhattanDistance)
                {
                    var d = record.ManhattanDistance - distanceOfSensorToRowToAnalyze;
                    horizontalIntervalsOnRowToAnalyze.Add((record.Sensor.x - d, record.Sensor.x + d));
                }
            }
            var start = horizontalIntervalsOnRowToAnalyze.Select(x => x.begin).Min(); 
            var end = horizontalIntervalsOnRowToAnalyze.Select(x => x.end).Max();
            var score = 0;
            var discard = sensorsPositionsAndClosestBeacon
                .Select(x => (x.Beacon.x, x.Beacon.y))
                .Concat(sensorsPositionsAndClosestBeacon
                .Select(x => (x.Sensor.x, x.Sensor.y)))
                .ToHashSet();
            for (var x = start; x <= end; x++)
            {
                var p = (x : x, y: verticalPositionOfRowToAnalyze);
                if (discard.Contains(p)) continue;
                foreach (var inter in horizontalIntervalsOnRowToAnalyze)
                {
                    if (x >= inter.begin && x <= inter.end)
                    {
                        score++;
                        break;
                    }
                }
            }
            return score.ToString();
        }
        public string SolveSecondPart(string puzzleInput)
        {
            var sensorsPositionsAndClosestBeacon = GetSensorPositionAndClosestBeacons(puzzleInput);
            var discard = sensorsPositionsAndClosestBeacon
                .Select(x => (x.Beacon.x, x.Beacon.y))
                .Concat(sensorsPositionsAndClosestBeacon
                .Select(x => (x.Sensor.x, x.Sensor.y)))
                .ToHashSet();
            var zones = new Queue<(int xMin, int yMin, int xMax, int yMax)>();
            var mapMaxSize = sensorsPositionsAndClosestBeacon.Count <= 14 ? 20 : 4000000;
            var maxIterations = (int)Math.Sqrt(mapMaxSize) + 3;
            zones.Enqueue((0, 0, mapMaxSize, mapMaxSize));
            do
            {
                var newZones = new Queue<(int xMin, int yMin, int xMax, int yMax)>();
                while (zones.Count > 0)
                {
                    var (ax, ay, bx, by) = zones.Dequeue();
                    var corners = new (int x, int y)[] { (ax, ay), (ax, by), (bx, by), (bx, ay) };
                    // intersection test
                    var isFullyCoveredBySensor = sensorsPositionsAndClosestBeacon.Any(x => corners.All(y => ManhattanDistance(x.Sensor, y) <= x.ManhattanDistance));
                    if (!isFullyCoveredBySensor)
                    {
                        var (dx, dy) = ((bx - ax + 1) / 2, (by - ay + 1) / 2);
                        if (dx > 0 || dy > 0)
                        {
                            newZones.Enqueue((ax, ay, bx - dx, by - dy));
                            newZones.Enqueue((ax + dx, ay, bx, by - dy));
                            newZones.Enqueue((ax, ay + dy, bx - dx, by));
                            newZones.Enqueue((ax + dx, ay + dy, bx, by));
                        }
                        else
                            if (!discard.Contains((ax, ay))) newZones.Enqueue((ax, ay, ax, ay));
                    }
                }
                zones = newZones;
            } while (zones.Count > 1 && maxIterations-- != 0);
            var res = zones.Dequeue();
            // too big for int
            return ((long)res.xMin * 4000000 + res.yMin).ToString();
        }
    }
}