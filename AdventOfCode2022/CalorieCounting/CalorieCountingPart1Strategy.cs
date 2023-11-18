namespace sylvain69780.AdventOfCode2022.Domain.CalorieCounting
{
    public class CalorieCountingPart1Strategy : ICalorieCountingStrategy
    {
        public IEnumerable<ProgressInfo> GetStepsToSolution(IEnumerable<int> caloriesHoldByElves,Func<int,ProgressInfo> updateContext,Action<string> provideSolution)
        {
            int maxCalories = 0;
            int currentSum = 0;
            foreach (var value in caloriesHoldByElves)
            {
                if (value == 0)
                    currentSum = 0;
                else
                {
                    currentSum += value;
                    if (currentSum > maxCalories)
                        maxCalories = currentSum;
                }
                yield return updateContext(currentSum);
            }
            provideSolution(maxCalories.ToString());
        }
    }
}
