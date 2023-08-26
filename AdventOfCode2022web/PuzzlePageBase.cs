using AdventOfCode2022web.Puzzles;
using Microsoft.AspNetCore.Components;

namespace AdventOfCode2022web
{
    public class PuzzlePageBase : ComponentBase
    {
        public virtual IIncrementalPuzzleSolver? PuzzleBase { get; protected set; }

        public bool PuzzleInputReloaded { get; set; }
        public int AnimationDuration { get; set; } = 500;
        public int SolvingStep { get; set; } = 0;

        public void NotifyStateHasChanged() => StateHasChanged();
    }
}
