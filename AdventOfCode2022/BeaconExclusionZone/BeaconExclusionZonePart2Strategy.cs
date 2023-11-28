using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BeaconExclusionZone
{
    public class BeaconExclusionZonePart2Strategy : IPuzzleStrategy<BeaconExclusionZoneModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(BeaconExclusionZoneModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var discard = model.SensorsPositionsAndClosestBeacon!
                .Select(x => (x.Beacon.x, x.Beacon.y))
                .Concat(model.SensorsPositionsAndClosestBeacon!
                .Select(x => (x.Sensor.x, x.Sensor.y)))
                .ToHashSet();
            var squares = new Queue<((int X, int Y) Min, (int X, int Y) Max)>();
            var mapMaxSize = model.SensorsPositionsAndClosestBeacon!.Count <= 14 ? 20 : 4000000;
            var maxIterations = (int)Math.Log2(mapMaxSize) + 1;
            squares.Enqueue(((0, 0), (mapMaxSize, mapMaxSize)));
            do
            {
                var subdividedSquares = new Queue<((int X, int Y) Min, (int X, int Y) Max)>();
                while (squares.Count > 0)
                {
                    var (min, max) = squares.Dequeue();
                    var isFullyCoveredBySensor = model.SensorsPositionsAndClosestBeacon
                        .Any(x => BeaconExclusionZoneModel.ManhattanDistance(x.Sensor, (min.X, min.Y)) <= x.ManhattanDistance
                            && BeaconExclusionZoneModel.ManhattanDistance(x.Sensor, (min.X, max.Y)) <= x.ManhattanDistance
                            && BeaconExclusionZoneModel.ManhattanDistance(x.Sensor, (max.X, max.Y)) <= x.ManhattanDistance
                            && BeaconExclusionZoneModel.ManhattanDistance(x.Sensor, (max.X, min.Y)) <= x.ManhattanDistance);
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
                yield return updateContext();
                // output.Put($"{squares.Count} quads evaluated with a side size of {squareSize}");

            } while (squares.Count > 1 && maxIterations-- != 0);
            var res = squares.Dequeue();
            // too big for int
            yield return updateContext();
            provideSolution(((long)res.Min.X * 4000000 + res.Min.Y).ToString());
        }
    }
}
