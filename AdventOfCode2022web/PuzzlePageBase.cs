using AdventOfCode2022web.Puzzles;
using Microsoft.AspNetCore.Components;

namespace AdventOfCode2022web
{
    public class PuzzlePageBase : ComponentBase
    {
        public PuzzleSolutionViewModel? PuzzleSolutionViewModel { get; protected set; }

        public int AnimationDuration { get; set; } = 500;
        public int SolvingStep { get; set; } = 0;

    }
}
