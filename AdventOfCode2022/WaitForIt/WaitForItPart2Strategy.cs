using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.WaitForIt
{
    public class WaitForItPart2Strategy : IPuzzleStrategy<WaitForItModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(WaitForItModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var time = long.Parse(string.Join("", model.Races.Select(x => x.time.ToString())));
            var distance = long.Parse(string.Join("", model.Races.Select(x => x.distance.ToString())));
            var range = WaitForItModel.FindRange(time, distance);
            yield return updateContext();
            provideSolution(range.ToString());
        }
    }
}
