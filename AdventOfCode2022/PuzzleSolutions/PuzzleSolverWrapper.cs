namespace AdventOfCode2022web.Puzzles
{
    public class PuzzleSolverWrapper : IPuzzleSolutionIter
    {
        private readonly IPuzzleSolution _puzzle;

        public PuzzleSolverWrapper(IPuzzleSolution puzzle) => _puzzle = puzzle;

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
