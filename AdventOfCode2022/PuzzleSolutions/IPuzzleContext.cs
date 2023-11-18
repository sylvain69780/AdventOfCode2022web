namespace AdventOfCode2022Solutions.PuzzleSolutions
{
    public interface IPuzzleContext
    {
        string Message { get; }
        int Step { get;  }

        IEnumerable<ProgressInfo> GetStepsToSolution(string input);
    }
}