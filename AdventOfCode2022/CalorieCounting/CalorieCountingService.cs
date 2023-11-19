using Domain.CalorieCounting;

namespace Domain.CalorieCounting
{
    public class CalorieCountingService : SimplePuzzleService<CalorieCountingModel>
    {
        public CalorieCountingService(IPuzzleStrategy<CalorieCountingModel> strategy) : base(strategy)
        {
        }

        public int CurrentSum => _model.SumsOfCalories[^1];
        public List<int> CaloriesHoldByElves => _model.CaloriesHoldByElves;
    }
}
