namespace sylvain69780.AdventOfCode2022.Domain
{
    public interface IPuzzleSolver
    {
        IEnumerable<PuzzleOutput> SolveFirstPart(string input);
        IEnumerable<PuzzleOutput> SolveSecondPart(string input);
    }
}
