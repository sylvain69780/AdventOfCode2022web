using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CampCleanup
{
    public class CampCleanupPart1Strategy : IPuzzleStrategy<CampCleanupModel>
    {
        public IEnumerable<ProcessingProgressModel> GetSteps(CampCleanupModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var score = 0;
            foreach (var (interval1, interval2) in model.ListOfSectionAssignmentPairs!)
            {
                if (interval1.Contains(interval2) || interval2.Contains(interval1))
                    score++;
                yield return updateContext();
            }
            provideSolution(score.ToString());
        }
    }
}
