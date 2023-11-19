namespace Domain
{
    public interface IPuzzleSolutionNEW
    {
        bool SolveStep();

        int Step { get; }
        string Message { get; }
    }
}
