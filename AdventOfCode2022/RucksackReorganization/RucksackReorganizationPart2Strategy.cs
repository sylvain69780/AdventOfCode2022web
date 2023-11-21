using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RucksackReorganization
{
    public class RucksackReorganizationPart2Strategy : IPuzzleStrategy<RuchsackReorganizationModel>
    {
        public IEnumerable<ProcessingProgressModel> GetSteps(RuchsackReorganizationModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var score = 0;
            for (var i = 0; i < model.Lines!.Length / 3; i++)
            {
                var (firstGroup, secondGroup, thirdGroup) = (model.Lines[i * 3], model.Lines[i * 3 + 1], model.Lines[i * 3 + 2]);
                var badge = firstGroup.First(x => secondGroup.Contains(x) && thirdGroup.Contains(x));
                score += Helpers.Priority(badge);
                yield return updateContext();
            }
            provideSolution(score.ToString());
        }
    }
}
