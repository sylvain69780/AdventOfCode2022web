using Domain.RockPaperScissors;

namespace Blazor.Pages
{
    public partial class RockPaperScissorsPage2
    {
        RockPaperScissorsService _puzzleService = new(new RockPaperScissorsPart2Strategy());
    }
}
