namespace Domain
{
    public abstract class PuzzleServiceBase
    {
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