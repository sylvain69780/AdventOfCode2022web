using System.Collections.Generic;

namespace AdventOfCode2022web.Domain.Puzzle
{
    public interface IPuzzleSolver
    {
        IAsyncEnumerable<string> Part1Async(string input);
        IAsyncEnumerable<string> Part2Async(string input);
    }
}
