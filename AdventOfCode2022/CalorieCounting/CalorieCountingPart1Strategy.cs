using Domain.CalorieCounting;

namespace Domain.CalorieCounting
{
    public class CalorieCountingPart1Strategy : IPuzzleStrategy<CalorieCountingModel>
    {
        public string Name { get; set; } = "Part 1";
        public IEnumerable<ProcessingProgressModel> GetSteps(CalorieCountingModel model,Func<ProcessingProgressModel> updateContext,Action<string> provideSolution)
        {
            foreach (var value in model.CaloriesHoldByElves)
            {
                if (value == 0)
                    model.CurrentSum = 0;
                else
                {
                    model.CurrentSum += value;
                    if (model.CurrentSum > model.SumsOfCalories[0])
                        model.SumsOfCalories[0] = model.CurrentSum;
                }
                yield return updateContext();
            }
            provideSolution(model.SumsOfCalories[0].ToString());
        }
    }
}
