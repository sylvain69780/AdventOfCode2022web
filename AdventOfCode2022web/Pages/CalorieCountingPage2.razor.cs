using sylvain69780.AdventOfCode2022.Domain.CalorieCounting;

namespace sylvain69780.AdventOfCode2022.Blazor.Pages
{
    public partial class CalorieCountingPage2
    {
        CalorieCountingModel _puzzleModel = new(new CalorieCountingPart2Strategy());
    }
}
