namespace AdventOfCode2022web.Domain.Puzzle
{
    public interface IPuzzleSolver
    {
        IEnumerable<string> SolveFirstPart(string input);
        IEnumerable<string> SolveSecondPart(string input);
    }
}
