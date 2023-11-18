using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting
{
    public class CalorieCountingSolutionNEW : CalorieCountingBase, IPuzzleSolutionNEW
    {
        public CalorieCountingSolutionNEW(string input) : base(input) {}

        protected override IEnumerable<ProgressInfo> Solve()
        {
            int maxCalories = 0;
            foreach (var value in _caloriesHoldByElves)
            {
                if (value == 0)
                    _currentSum = 0;
                else
                {
                    _currentSum += value;
                    if ( _currentSum > maxCalories)
                    {
                        maxCalories = _currentSum;
                        _message = maxCalories.ToString();
                    }
                }
                yield return MarkProgress();
            }
        }
    }
}
