using sylvain69780.AdventOfCode2022.Domain.CalorieCounting;

namespace AdventOfCode2022web.Pages
{
    public partial class CalorieCountingPage2
    {
        CalorieCountingModel _puzzleModel = new(new CalorieCountingPart2Strategy());
    }
}
