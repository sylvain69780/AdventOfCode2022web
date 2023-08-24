using AdventOfCode2022web.Puzzles;
using Microsoft.AspNetCore.Components;

namespace AdventOfCode2022web
{
    public class PuzzlePageBase : ComponentBase
    {
        public virtual IIncrementalPuzzleSolver? PuzzleBase { get; private set; }

        public bool PuzzleInputReloaded { get; set; }
        public int AnimationDuration = 500;

        public void NotifyStateHasChanged() => StateHasChanged();
    }
}
