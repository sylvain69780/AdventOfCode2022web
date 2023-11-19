namespace Domain
{
    public class SimplePuzzleService<TModel> : IPuzzleService where TModel : IPuzzleModel,new()
    {
        protected readonly TModel _model;
        protected readonly IPuzzleStrategy<TModel> _strategy;
        public SimplePuzzleService(IPuzzleStrategy<TModel> strategy)
        {
            _strategy = strategy;
            _model = new();
        }
        public IEnumerable<ProcessingProgressModel> GetStepsToSolution(string input)
        {
            _model.Parse(input);
            _progress = new ProcessingProgressModel();
            return _strategy.GetSteps(_model, Update, ProvideSolution);
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
