using System.ComponentModel;

namespace AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting
{
    public class CalorieCountingModel : IPuzzleModel
    {
        ICalorieCountingStrategy _strategy;
        int _currentSum;
        List<int> _caloriesHoldByElves = new();
        string _message;
        ProgressInfo _progress;
        public CalorieCountingModel(ICalorieCountingStrategy strategy)
        {
            _strategy = strategy;
            _message = string.Empty;
            _progress = new ProgressInfo();
        }

        public IEnumerable<ProgressInfo> GetStepsToSolution(string input)
        {
            _currentSum = 0;
            _caloriesHoldByElves = input.Split('\n').Select(x => int.TryParse(x, out var value) ? value : 0).ToList();
            _progress.Step = 0;
            return _strategy.GetStepsToSolution(_caloriesHoldByElves, Update, ProvideSolution);
        }

        private ProgressInfo Update(int currentSum)
        {
            _progress.Step++;
            _currentSum = currentSum;
            return _progress;
        }

        private void ProvideSolution(string solution)
        {
            _message = solution;
        }

        public int CurrentSum { get => _currentSum; set { _currentSum = value; } }
        public List<int> CaloriesHoldByElves => _caloriesHoldByElves;
        public string Message { get => _message; set { _message = value; } }
        public int Step => _progress.Step;
    }
}
