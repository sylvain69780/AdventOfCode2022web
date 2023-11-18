namespace AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting
{
    abstract public class CalorieCountingBase : PuzzleBase
    {
        public int CurrentSum => _currentSum;
        public List<int> CaloriesHoldByElves => _caloriesHoldByElves;

        protected int _currentSum = 0;
        protected List<int> _caloriesHoldByElves;

        public CalorieCountingBase(string input) : base(input)
        {
            _currentSum = 0;
            _caloriesHoldByElves = input.Split('\n').Select(x => int.TryParse(x, out var value) ? value : 0).ToList();
        }
    }
}
