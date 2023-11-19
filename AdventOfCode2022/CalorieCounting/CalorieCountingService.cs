using Domain.CalorieCounting;

namespace Domain.CalorieCounting
{
    public class CalorieCountingService : PuzzleServiceBase, IPuzzleService
    {
        CalorieCountingModel _model = new();
        ICalorieCountingStrategy _strategy;

        public int CurrentSum => _model.SumsOfCalories[^1];
        public List<int> CaloriesHoldByElves => _model.CaloriesHoldByElves;

        public CalorieCountingService(ICalorieCountingStrategy strategy)
        {
            _strategy = strategy;
        }

        public IEnumerable<ProcessingProgressModel> GetStepsToSolution(string input)
        {
            _model.Parse(input);
            _progress = new ProcessingProgressModel();
            return _strategy.GetSteps(_model, Update, ProvideSolution);
        }
    }
}
