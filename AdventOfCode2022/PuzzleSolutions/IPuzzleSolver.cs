namespace AdventOfCode2022Solutions.PuzzleSolutions
{
    public interface IPuzzleSolver
    {
        IEnumerable<PuzzleOutput> SolveFirstPart(string input);
        IEnumerable<PuzzleOutput> SolveSecondPart(string input);
    }
}
