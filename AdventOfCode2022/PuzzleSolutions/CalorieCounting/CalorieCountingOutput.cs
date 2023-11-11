namespace AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting
{
    public class CalorieCountingOutput : PuzzleOutput
    {
        public int CurrentSum { get; set; }
        public List<int> CaloriesHoldByElves { get; set; } = new();
    }
}
