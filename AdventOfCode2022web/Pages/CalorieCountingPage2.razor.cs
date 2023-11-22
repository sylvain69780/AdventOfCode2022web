using Domain.CalorieCounting;

namespace Blazor.Pages
{
    public partial class CalorieCountingPage2
    {
        CalorieCountingService _puzzleService = new(new CalorieCountingPart2Strategy());
    }
}
