using Microsoft.AspNetCore.Components;

namespace AdventOfCode2022web
{
    public class PuzzleViewBase : ComponentBase
    {
        [CascadingParameter(Name = "PuzzleSolutionViewModel")]
        protected PuzzleSolutionViewModel? PuzzleSolutionViewModel { get; set; }

        protected int AnimationDuration => PuzzleSolutionViewModel?.AnimationDuration ?? 500;
    }
}
