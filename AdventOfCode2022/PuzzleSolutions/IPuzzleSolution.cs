namespace AdventOfCode2022Solutions.PuzzleSolutions
{
    public interface IPuzzleSolutionNEW
    {
        bool SolveStep();

        int Step { get; }
        string Message { get; }
    }
}
