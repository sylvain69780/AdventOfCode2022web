namespace AdventOfCode2022Solutions.PuzzleSolutions
{
    public interface IPuzzleModel
    {
        string Message { get; }
        int Step { get;  }

        IEnumerable<ProgressInfo> GetStepsToSolution(string input);
    }
}