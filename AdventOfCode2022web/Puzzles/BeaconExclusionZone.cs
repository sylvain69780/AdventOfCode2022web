﻿using System.Text.RegularExpressions;

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
                var p = (x: x, y: verticalPositionOfRowToAnalyze);
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
            var squares = new Queue<((int X, int Y) Min, (int X, int Y) Max)>();
            var mapMaxSize = sensorsPositionsAndClosestBeacon.Count <= 14 ? 20 : 4000000;
            var maxIterations = (int)Math.Log2(mapMaxSize)+1; 
            squares.Enqueue(((0, 0), (mapMaxSize, mapMaxSize)));
            do
            {
                var smallerSquares = new Queue<((int X, int Y) Min, (int X, int Y) Max)>();
                while (squares.Count > 0)
                {
                    var (min, max) = squares.Dequeue();
                    var corners = new (int x, int y)[] { (min.X, min.Y), (min.X, max.Y), (max.X, max.Y), (max.X, min.Y) };
                    var isFullyCoveredBySensor = sensorsPositionsAndClosestBeacon
                        .Any(x => corners.All(y => ManhattanDistance(x.Sensor, y) <= x.ManhattanDistance));
                    if (!isFullyCoveredBySensor)
                    {
                        var (width, height) = ((max.X - min.X), (max.Y - min.Y));
                        var (firstHalfX, firstHalfY) = (width / 2, height / 2);
                        var (secondHalfX, secondHalfY) = (width - firstHalfX, height - firstHalfY );
                        if (width == 0 && height == 0)
                        {
                            // success
                            if (!discard.Contains((min.X, min.Y)))
                            {
                                smallerSquares.Clear();
                                smallerSquares.Enqueue((min, min));
                                break;
                            }
                                
                        }
                        else
                            {
                                smallerSquares.Enqueue(
                                    (min, 
                                    (min.X + firstHalfX, min.Y + firstHalfY)
                                    ));
                                smallerSquares.Enqueue(
                                    ((min.X+secondHalfX,min.Y+secondHalfY),
                                    (max.X, max.Y)
                                    ));
                                smallerSquares.Enqueue(
                                    ((min.X + secondHalfX, min.Y ),
                                    (max.X, min.Y + firstHalfY)
                                    ));
                                smallerSquares.Enqueue(
                                    ((min.X , min.Y + secondHalfY),
                                    (min.X + firstHalfX, max.Y)
                                    ));
                            }
                    }
                }
                squares = smallerSquares;
            } while (squares.Count > 1 && maxIterations-- != 0);
            var res = squares.Dequeue();
            // too big for int
            return ((long)res.Min.X * 4000000 + res.Min.Y).ToString();
        }
    }
}