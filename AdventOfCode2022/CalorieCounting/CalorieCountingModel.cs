namespace Domain.CalorieCounting
{
    public class CalorieCountingModel : IPuzzleModel
    {
        public List<int> CaloriesHoldByElves = new();
        public int CurrentSum { get; set; }
        public List<int> SumsOfCalories { get; set; } = new() { 0 };
        public void Parse(string input)
        {
            CurrentSum = 0;
            CaloriesHoldByElves = input.Split('\n').Select(x => int.TryParse(x, out var value) ? value : 0).ToList();
        }
    }
}
