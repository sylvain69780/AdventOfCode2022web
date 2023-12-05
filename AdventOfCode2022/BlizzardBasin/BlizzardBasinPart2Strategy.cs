using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BlizzardBasin
{
    public class BlizzardBasinPart2Strategy : IPuzzleStrategy<BlizzardBasinModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(BlizzardBasinModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            yield return updateContext();
            provideSolution("");
        }
    }
}
