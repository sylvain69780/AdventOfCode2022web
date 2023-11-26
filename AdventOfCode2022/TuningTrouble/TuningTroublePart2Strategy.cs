using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TuningTrouble
{
    public class TuningTroublePart2Strategy : IPuzzleStrategy<TuningTroubleModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(TuningTroubleModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            yield return updateContext();
            provideSolution(model.FindMarkerPosition(14).ToString());
        }
    }
}
