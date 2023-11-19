using Domain.CalorieCounting;

namespace Blazor.Pages
{
    public partial class CalorieCountingPage
    {
        CalorieCountingService _puzzleService = new(new CalorieCountingPart1Strategy());
    }
}
