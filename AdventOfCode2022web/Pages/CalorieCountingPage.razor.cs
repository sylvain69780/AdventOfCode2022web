using Domain.CalorieCounting;

namespace Blazor.Pages
{
    public partial class CalorieCountingPage
    {
        CalorieCountingService _puzzleModel = new(new CalorieCountingPart1Strategy());
    }
}
