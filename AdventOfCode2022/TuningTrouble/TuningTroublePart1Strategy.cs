using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TuningTrouble
{
    public class TuningTroublePart1Strategy : IPuzzleStrategy<TuningTroubleModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(TuningTroubleModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            yield return updateContext();
            provideSolution(model.FindMarkerPosition(4).ToString());
        }
    }
}
