namespace Domain
{
    public class SimplePuzzleService<TModel> : IPuzzleService where TModel : IPuzzleModel,new()
    {
        protected readonly TModel _model = new();
        protected readonly Dictionary<string,IPuzzleStrategy<TModel>> _strategies = new();
        protected string _currentStrategy = string.Empty;
        public SimplePuzzleService(IPuzzleStrategy<TModel> strategy)
        {
            _strategies.Add("Default",strategy);
            _currentStrategy = "Default";
        }
        public SimplePuzzleService(IEnumerable<IPuzzleStrategy<TModel>> strategies)
        {
            foreach(var strategy in strategies)
                _strategies.Add(strategy.Name, strategy);
            _currentStrategy = strategies.First().Name;
        }

        public string CurrentStrategy => _currentStrategy;
        public void SetStrategy(string strategyName)
        {
            _currentStrategy = strategyName;
        }
        public IEnumerable<string> Strategies => _strategies.Select(x => x.Key);
        public IEnumerable<ProcessingProgressModel> GetStepsToSolution(string input)
        {
            _model.Parse(input);
            _progress = new ProcessingProgressModel();
            return _strategies[_currentStrategy].GetSteps(_model, Update, ProvideSolution);
        }

        protected ProcessingProgressModel _progress = new();
        public string Message => _progress.Message;
        public string Solution => _progress.Solution;
        public int Step => _progress.Step;
        protected ProcessingProgressModel Update()
        {
            _progress.Step++;
            return _progress;
        }

        protected void ProvideSolution(string solution)
        {
            _progress.Solution = solution;
        }
    }
}
