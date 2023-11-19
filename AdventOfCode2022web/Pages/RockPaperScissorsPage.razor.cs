using Domain.RockPaperScissors;

namespace Blazor.Pages
{
    public partial class RockPaperScissorsPage
    {
        RockPaperScissorsService _puzzleService = new(new RockPaperScissorsPart1Strategy());
    }
}
