namespace AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting
{
    public class CalorieCountingDTO : IPuzzleSolverDTO
    {
        public int Step { get; set; }
        public string Output { get; set; } = string.Empty;
        public int CurrentSum { get; set; }
        public List<int> CaloriesHoldByElves { get; set; } = new();
    }
}
