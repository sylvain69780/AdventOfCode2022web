
namespace AdventOfCode2022web.Domain.Puzzle
{
    public abstract class PuzzleSolver : IPuzzleSolver
    {
        public virtual async IAsyncEnumerable<string> Part1Async(string input)
        {
            yield return SolveFirst(input);
            await Task.Delay(1);
        }
        public virtual async IAsyncEnumerable<string> Part2Async(string input)
        {
            yield return SolveSecond(input);
            await Task.Delay(1);
        }
        protected abstract string SolveFirst(string input);
        protected abstract string SolveSecond(string input);
    }
}
