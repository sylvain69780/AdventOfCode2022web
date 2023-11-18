using sylvain69780.AdventOfCode2022.Domain.CalorieCounting;

namespace AdventOfCode2022web.Pages
{
    public partial class CalorieCountingPage
    {
        CalorieCountingModel _puzzleModel = new(new CalorieCountingPart1Strategy());
    }
}
