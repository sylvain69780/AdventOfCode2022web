using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;

namespace AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting
{
    public class CalorieCountingPart2Strategy : ICalorieCountingStrategy
    {
        public IEnumerable<ProgressInfo> GetStepsToSolution(IEnumerable<int> caloriesHoldByElves,Func<int,ProgressInfo> updateContext,Action<string> provideSolution)
        {
            var sumsOfCalories = new List<int>() { 0 };
            foreach (var value in caloriesHoldByElves)
            {
                if (value == 0)
                    sumsOfCalories.Add(0);
                else
                    sumsOfCalories[^1] += value;
                yield return updateContext(sumsOfCalories.OrderByDescending(x => x).Take(3).Sum());
            }
            provideSolution(sumsOfCalories.OrderByDescending(x => x).Take(3).Sum().ToString());
        }
    }
}
