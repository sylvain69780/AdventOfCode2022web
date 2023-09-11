using Microsoft.AspNetCore.Components;

namespace AdventOfCode2022web
{
    public class PuzzleSolutionPage : ComponentBase
    {
        public int AnimationDuration { get; set; } = 500;
        public int SolvingStep { get; set; } = 0;

        public Action? PuzzleOutputReturned { get; set; }
        public void NotifyPuzzleOutputReturned() => PuzzleOutputReturned?.Invoke();

        public Action? PuzzleInputNeeded { get; set; }
        public void NotifyPuzzleInputNeeded() => PuzzleInputNeeded?.Invoke();

        public Action? PuzzleInputLoaded { get; set; }
        public void NotifyPuzzleInputLoaded() => PuzzleInputLoaded?.Invoke();
    }
}
