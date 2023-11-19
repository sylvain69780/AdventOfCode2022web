namespace Domain
{
    public class SimplePuzzleService<TModel> : PuzzleServiceBase where TModel : IPuzzleModel,new()
    {
        readonly TModel _model;
        readonly ISimplePuzzleStrategy<TModel> _strategy;
        public SimplePuzzleService(ISimplePuzzleStrategy<TModel> strategy)
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
    }
}
