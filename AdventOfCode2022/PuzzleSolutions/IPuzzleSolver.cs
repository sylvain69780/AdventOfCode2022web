namespace AdventOfCode2022Solutions.PuzzleSolutions
{
    public interface IPuzzleSolver
    {
        IEnumerable<IPuzzleSolverDTO> SolveFirstPart(string input);
        IEnumerable<IPuzzleSolverDTO> SolveSecondPart(string input);
    }
}
