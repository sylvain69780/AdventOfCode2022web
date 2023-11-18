namespace sylvain69780.AdventOfCode2022.Domain
{
    public interface IPuzzleModel
    {
        string Message { get; }
        int Step { get;  }

        IEnumerable<ProgressInfo> GetStepsToSolution(string input);
    }
}