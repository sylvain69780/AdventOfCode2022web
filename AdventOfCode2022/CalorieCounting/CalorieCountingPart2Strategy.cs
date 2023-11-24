using Domain.CalorieCounting;

namespace Domain.CalorieCounting
{
    public class CalorieCountingPart2Strategy : IPuzzleStrategy<CalorieCountingModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(CalorieCountingModel model,Func<ProcessingProgressModel> updateContext,Action<string> provideSolution)
        {
            foreach (var value in model.CaloriesHoldByElves)
            {
                if (value == 0)
                    model.SumsOfCalories.Add(0);
                else
                    model.SumsOfCalories[^1] += value;
                yield return updateContext();
            }
            provideSolution(model.SumsOfCalories.OrderByDescending(x => x).Take(3).Sum().ToString());
        }
    }
}
