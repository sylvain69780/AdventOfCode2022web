using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PointOfIIcidence
{
    public class PointOfIIcidencePart2Strategy : IPuzzleStrategy<PointOfIIcidenceModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(PointOfIIcidenceModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            yield return updateContext();
            provideSolution("");
        }
    }
}
