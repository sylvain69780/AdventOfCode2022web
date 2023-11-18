using AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting;

namespace AdventOfCode2022web.Pages
{
    public partial class CalorieCountingPage
    {
        CalorieCountingModel _puzzleModel = new(new CalorieCountingPart1Strategy());
    }
}
