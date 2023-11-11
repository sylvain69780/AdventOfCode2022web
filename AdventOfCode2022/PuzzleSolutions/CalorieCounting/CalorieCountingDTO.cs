namespace AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting
{
    public class CalorieCountingDTO : PuzzleSolverDTO
    {
        public int CurrentSum { get; set; }
        public List<int> CaloriesHoldByElves { get; set; } = new();
    }
}
