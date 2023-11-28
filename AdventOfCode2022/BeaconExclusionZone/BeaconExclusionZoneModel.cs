using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.BeaconExclusionZone
{
    public class BeaconExclusionZoneModel : IPuzzleModel
    {
        public void Parse(string input)
        {
            _sensorsPositionsAndClosestBeacon = GetSensorPositionAndClosestBeacons(input);
        }

        public List<SensorPositionAndClosestBeacon>? _sensorsPositionsAndClosestBeacon;
        public List<SensorPositionAndClosestBeacon>? SensorsPositionsAndClosestBeacon => _sensorsPositionsAndClosestBeacon;
        public static int ManhattanDistance((int x, int y) a, (int x, int y) b) => Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);

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

    }
}
