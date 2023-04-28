using System.Collections.Generic;

namespace AdventOfCode2022web.Domain.Puzzle
{
    public abstract class PuzzleSolver : IPuzzleSolver
    {
        public virtual async IAsyncEnumerable<string> Part1Async(string input)
        {
            yield return Part1(input);
            await Task.Delay(1);
        }
        public virtual async IAsyncEnumerable<string> Part2Async(string input)
        {
            yield return Part2(input);
            await Task.Delay(1);
        }
        protected abstract string Part1(string input);
        protected abstract string Part2(string input);
    }
}
