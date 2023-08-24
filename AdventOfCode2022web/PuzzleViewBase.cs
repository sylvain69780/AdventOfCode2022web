using Microsoft.AspNetCore.Components;

namespace AdventOfCode2022web
{
    public class PuzzleViewBase : ComponentBase
    {
        [CascadingParameter(Name = "Parent")]
        protected PuzzlePageBase? Parent { get; set; }
        [Parameter]
        public int AnimationDuration { get; set; } = 500;


    }
}
