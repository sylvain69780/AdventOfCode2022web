namespace Domain
{
    public interface IPuzzleService
    {
        IEnumerable<ProcessingProgressModel> GetStepsToSolution(string input);
        public string CurrentStrategy { get; }
        public void SetStrategy(string strategyName);
        public IEnumerable<string> Strategies { get; }

        int Step { get; }
        string Message { get; }
        string Solution { get; }
    }
}