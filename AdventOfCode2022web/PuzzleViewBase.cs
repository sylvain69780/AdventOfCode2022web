using AdventOfCode2022Solutions.PuzzleSolutions;
using Microsoft.AspNetCore.Components;

namespace AdventOfCode2022web
{
    public class PuzzleViewBase : ComponentBase
    {
        [CascadingParameter(Name = "PuzzleSolutionPage")]
        protected PuzzleSolutionPage? PuzzleSolutionPage { get; set; }
        [CascadingParameter(Name = "PuzzleSolution")]
        protected IPuzzleSolutionIter? PuzzleSolution { get; set; }

        protected int AnimationDuration => PuzzleSolutionPage?.AnimationDuration ?? 500;
    }
}
