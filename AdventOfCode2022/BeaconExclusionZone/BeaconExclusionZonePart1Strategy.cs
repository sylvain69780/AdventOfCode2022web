using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BeaconExclusionZone
{
    public class BeaconExclusionZonePart1Strategy : IPuzzleStrategy<BeaconExclusionZoneModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(BeaconExclusionZoneModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var verticalPositionOfRowToAnalyze = model.SensorsPositionsAndClosestBeacon!.Count <= 14 ? 10 : 2000000;
            var horizontalIntervalsOnRowToAnalyze = new List<(int begin, int end)>();
            foreach (var record in model.SensorsPositionsAndClosestBeacon)
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
            var discard = model.SensorsPositionsAndClosestBeacon
                .Select(x => (x.Beacon.x, x.Beacon.y))
                .Concat(model.SensorsPositionsAndClosestBeacon
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
            yield return updateContext();
            provideSolution(score.ToString());
        }
    }
}
