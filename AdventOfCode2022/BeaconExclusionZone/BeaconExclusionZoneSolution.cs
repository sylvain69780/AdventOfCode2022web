using System.Text.RegularExpressions;

namespace sylvain69780.AdventOfCode2022.Domain.BeaconExclusionZone
{
    public class BeaconExclusionZoneSolution : IPuzzleSolver
    {
        private List<SensorPositionAndClosestBeacon>? _sensorsPositionsAndClosestBeacon;

        private static int ManhattanDistance((int x, int y) a, (int x, int y) b) => Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);

        private static List<SensorPositionAndClosestBeacon> GetSensorPositionAndClosestBeacons(string puzzleInput)
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

        public IEnumerable<PuzzleOutput> SolveFirstPart(string input)
        {
            var output = new PuzzleOutputProvider();
            yield return output.Put("Start");
            _sensorsPositionsAndClosestBeacon = GetSensorPositionAndClosestBeacons(input);
            var verticalPositionOfRowToAnalyze = _sensorsPositionsAndClosestBeacon!.Count <= 14 ? 10 : 2000000;
            var horizontalIntervalsOnRowToAnalyze = new List<(int begin, int end)>();
            foreach (var record in _sensorsPositionsAndClosestBeacon)
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
            var discard = _sensorsPositionsAndClosestBeacon
                .Select(x => (x.Beacon.x, x.Beacon.y))
                .Concat(_sensorsPositionsAndClosestBeacon
                .Select(x => (x.Sensor.x, x.Sensor.y)))
                .ToHashSet();
            for (var x = start; x <= end; x++)
            {
                var p = (x, y: verticalPositionOfRowToAnalyze);
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
            yield return output.Put(score.ToString());
        }
        public IEnumerable<PuzzleOutput> SolveSecondPart(string input)
        {
            var output = new PuzzleOutputProvider();
            yield return output.Put("Start");
            _sensorsPositionsAndClosestBeacon = GetSensorPositionAndClosestBeacons(input);
            var discard = _sensorsPositionsAndClosestBeacon!
                .Select(x => (x.Beacon.x, x.Beacon.y))
                .Concat(_sensorsPositionsAndClosestBeacon!
                .Select(x => (x.Sensor.x, x.Sensor.y)))
                .ToHashSet();
            var squares = new Queue<((int X, int Y) Min, (int X, int Y) Max)>();
            var mapMaxSize = _sensorsPositionsAndClosestBeacon!.Count <= 14 ? 20 : 4000000;
            var maxIterations = (int)Math.Log2(mapMaxSize) + 1;
            squares.Enqueue(((0, 0), (mapMaxSize, mapMaxSize)));
            do
            {
                var subdividedSquares = new Queue<((int X, int Y) Min, (int X, int Y) Max)>();
                while (squares.Count > 0)
                {
                    var (min, max) = squares.Dequeue();
                    var isFullyCoveredBySensor = _sensorsPositionsAndClosestBeacon
                        .Any(x => ManhattanDistance(x.Sensor, (min.X, min.Y)) <= x.ManhattanDistance
                            && ManhattanDistance(x.Sensor, (min.X, max.Y)) <= x.ManhattanDistance
                            && ManhattanDistance(x.Sensor, (max.X, max.Y)) <= x.ManhattanDistance
                            && ManhattanDistance(x.Sensor, (max.X, min.Y)) <= x.ManhattanDistance);
                    if (!isFullyCoveredBySensor)
                    {
                        var (width, height) = (max.X - min.X, max.Y - min.Y);
                        var (firstHalfX, firstHalfY) = (width / 2, height / 2);
                        var (secondHalfX, secondHalfY) = (width - firstHalfX, height - firstHalfY);
                        if (width == 0 && height == 0)
                        {
                            // success
                            if (!discard.Contains((min.X, min.Y)))
                            {
                                subdividedSquares.Clear();
                                subdividedSquares.Enqueue((min, min));
                                break;
                            }

                        }
                        else
                        {
                            subdividedSquares.Enqueue(
                                (min,
                                (min.X + firstHalfX, min.Y + firstHalfY)
                                ));
                            if (secondHalfX > 0 && secondHalfY > 0)
                                subdividedSquares.Enqueue(
                                    ((min.X + secondHalfX, min.Y + secondHalfY),
                                    (max.X, max.Y)
                                    ));
                            if (secondHalfX > 0)
                                subdividedSquares.Enqueue(
                                    ((min.X + secondHalfX, min.Y),
                                    (max.X, min.Y + firstHalfY)
                                    ));
                            if (secondHalfY > 0)
                                subdividedSquares.Enqueue(
                                    ((min.X, min.Y + secondHalfY),
                                    (min.X + firstHalfX, max.Y)
                                    ));
                        }
                    }
                }
                squares = subdividedSquares;
                var (Min, Max) = squares.Peek();
                var squareSize = Max.X - Min.X + 1;
                yield return output.Put($"{squares.Count} quads evaluated with a side size of {squareSize}");

            } while (squares.Count > 1 && maxIterations-- != 0);
            var res = squares.Dequeue();
            // too big for int
            yield return output.Put(((long)res.Min.X * 4000000 + res.Min.Y).ToString());
        }
    }
}