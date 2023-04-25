namespace AdventOfCode2022web.Domain.Puzzle
{
    public interface IPuzzleSolver
    {
        string Input { get; set; }
        string Part1();
        string Part2();
    }
}
