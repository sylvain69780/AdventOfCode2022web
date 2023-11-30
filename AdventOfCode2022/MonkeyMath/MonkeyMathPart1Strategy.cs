using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MonkeyMath
{
    public class MonkeyMathPart1Strategy : IPuzzleStrategy<MonkeyMathModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(MonkeyMathModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            yield return updateContext();
            provideSolution(MonkeyMathModel.GetYelledNumber(model.JobOfEachMonkey!, "root").ToString());
        }
    }
}
