namespace AdventOfCode2022Solutions.PuzzleSolutions
{
    public interface IPuzzleSolver
    {
        IEnumerable<PuzzleSolverDTO> SolveFirstPart(string input);
        IEnumerable<PuzzleSolverDTO> SolveSecondPart(string input);
    }
}
