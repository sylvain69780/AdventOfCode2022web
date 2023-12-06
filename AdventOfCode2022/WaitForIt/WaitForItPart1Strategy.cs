using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.WaitForIt
{
    public class WaitForItPart1Strategy : IPuzzleStrategy<WaitForItModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(WaitForItModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var races = model.Races;
            var waysToBeatRecord = new List<long>();
            foreach (var (time, distance) in races)
            {
                long range = WaitForItModel.FindRange(time, distance);

                waysToBeatRecord.Add(range);
            }
            yield return updateContext();
            provideSolution(waysToBeatRecord.Aggregate(1L, (x, y) => x * y).ToString());
        }

    }
}
