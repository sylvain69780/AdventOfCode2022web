namespace Domain
{
    public interface IPuzzleService
    {
        IEnumerable<ProcessingProgressModel> GetStepsToSolution(string input);

        int Step { get; }
        string Message { get; }
        string Solution { get; }
    }
}