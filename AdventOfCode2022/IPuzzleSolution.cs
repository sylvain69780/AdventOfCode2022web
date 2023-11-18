namespace sylvain69780.AdventOfCode2022.Domain
{
    public interface IPuzzleSolutionNEW
    {
        bool SolveStep();

        int Step { get; }
        string Message { get; }
    }
}
