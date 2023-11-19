using Domain.CalorieCounting;

namespace Domain.CalorieCounting
{
    public class CalorieCountingPart1Strategy : IPuzzleStrategy<CalorieCountingModel>
    {
        public IEnumerable<ProcessingProgressModel> GetSteps(CalorieCountingModel model,Func<ProcessingProgressModel> updateContext,Action<string> provideSolution)
        {
            foreach (var value in model.CaloriesHoldByElves)
            {
                if (value == 0)
                    model.CurrentSum = 0;
                else
                {
                    var currentSum = model.CurrentSum;
                    currentSum += value;
                    if (currentSum > model.SumsOfCalories[0])
                        model.SumsOfCalories[0] = currentSum;
                }
                yield return updateContext();
            }
            provideSolution(model.SumsOfCalories[0].ToString());
        }
    }
}
