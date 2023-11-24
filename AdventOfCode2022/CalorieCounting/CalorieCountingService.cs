using Domain.CalorieCounting;

namespace Domain.CalorieCounting
{
    public class CalorieCountingService : SimplePuzzleService<CalorieCountingModel>
    {

        public CalorieCountingService(IEnumerable<IPuzzleStrategy<CalorieCountingModel>> strategies) : base(strategies)
        {
        }

        public int CurrentSum => _model.SumsOfCalories[^1];
        public List<int> CaloriesHoldByElves => _model.CaloriesHoldByElves;
    }
}
