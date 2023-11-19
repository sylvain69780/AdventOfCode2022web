using Domain.CalorieCounting;

namespace Blazor.Pages
{
    public partial class CalorieCountingPage2
    {
        CalorieCountingService _puzzleModel = new(new CalorieCountingPart2Strategy());
    }
}
