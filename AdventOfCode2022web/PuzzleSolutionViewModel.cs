using AdventOfCode2022web.Puzzles;

namespace AdventOfCode2022web
{
    public class PuzzleSolutionViewModel
    {
        public IPuzzleSolutionIter PuzzleSolution { get; private set; }

        public PuzzleSolutionViewModel(IPuzzleSolutionIter solution)
        {
            PuzzleSolution = solution;
        }

        public int AnimationDuration { get; set; } = 500;
        public int SolvingStep { get; set; } = 0;

        public Action? PuzzleOutputReturned { get; set; }

        public void NotifyStateHasChanged() 
        {
            PuzzleOutputReturned?.Invoke();
        }
    }
}
