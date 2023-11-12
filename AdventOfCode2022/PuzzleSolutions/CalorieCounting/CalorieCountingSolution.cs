using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting
{
    [PuzzleSolution(1)]
    public class CalorieCountingSolution : IPuzzleSolver
    {
        private static List<int> ParseInput(string input)
            => input.Split('\n').Select(x => int.TryParse(x, out var value) ? value : 0).ToList();
        public IEnumerable<PuzzleOutput> SolveFirstPart(string input)
        {
            var output = new PuzzleOutputProvider();
            var info = new CalorieCountingInfo()
            {
                CaloriesHoldByElves = ParseInput(input),
                CurrentSum = 0
            };
            int maxCalories = 0;
            yield return output.Put("Start", info);
            foreach (var value in input.Split('\n'))
            {
                if (value == string.Empty)
                    info.CurrentSum = 0;
                else
                {
                    info.CurrentSum += int.Parse(value);
                    maxCalories = Math.Max(maxCalories, info.CurrentSum);
                }
                var msg = $"The current group of Elves carries {info.CurrentSum} calories.\nCurrent max value is {maxCalories}";
                yield return output.Put(msg, info);
            }
            yield return output.Put(maxCalories.ToString(), info);
        }
        public IEnumerable<PuzzleOutput> SolveSecondPart(string input)
        {
            var output = new PuzzleOutputProvider();
            var info = new CalorieCountingInfo()
            {
                CaloriesHoldByElves = ParseInput(input),
                CurrentSum = 0
            };
            var sumsOfCalories = new List<int>() { 0 };
            yield return output.Put("Start", info);
            foreach (var value in info.CaloriesHoldByElves)
            {
                if (value == 0)
                    sumsOfCalories.Add(0);
                else
                    sumsOfCalories[^1] += value;
                info.CurrentSum = sumsOfCalories[^1]; // for the visualization
                var msg = "Top 3 of Elves groups holding the more calories:\n" + string.Join('\n', sumsOfCalories.OrderByDescending(x => x).Take(3).Select(x => x.ToString()));
                yield return output.Put(msg, info);
            }
            yield return output.Put(sumsOfCalories.OrderByDescending(x => x).Take(3).Sum().ToString(), info);
        }
    }
}
