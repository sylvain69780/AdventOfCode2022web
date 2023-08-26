namespace AdventOfCode2022web.Puzzles
{
    public class PuzzleSolutionWrapper : IPuzzleSolutionIter
    {
        private readonly IPuzzleSolution _puzzle;

        public PuzzleSolutionWrapper(IPuzzleSolution puzzle) => _puzzle = puzzle;

        public Type PuzzleType => _puzzle.GetType();

        public void Initialize(string puzzleInput)
        {
            _puzzle!.Initialize(puzzleInput);
        }
        public IEnumerable<string> SolveFirstPart()
        {
            yield return _puzzle.SolveFirstPart();
        }
        public IEnumerable<string> SolveSecondPart()
        {
            yield return _puzzle.SolveSecondPart();
        }
    }
}
