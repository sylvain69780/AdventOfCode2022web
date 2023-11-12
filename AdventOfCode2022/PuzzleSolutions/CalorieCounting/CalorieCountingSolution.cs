using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting
{
    [PuzzleSolution(1)]
    public class CalorieCountingSolution : IPuzzleSolver
    {
        private List<int> _caloriesHoldByElves = new();

        private static int CaloriesCarriedOrZero(string s)
        {
            if (int.TryParse(s, out var value)) 
                return value;
            else
                return 0;
        }
        private void ParseInput(string input)
        {

            _caloriesHoldByElves = input.Split('\n')
                .Select(x => CaloriesCarriedOrZero(x)).ToList();
        }
        public IEnumerable<PuzzleOutput> SolveFirstPart(string input)
        {
            ParseInput(input);
            int sumOfCalories = 0;
            int maxCalories = 0;
            var output = new CalorieCountingOutputProvider();
            yield return output.Put("Start", _caloriesHoldByElves, sumOfCalories);
            foreach (var value in input.Split('\n'))
            {
                if (value == string.Empty)
                    sumOfCalories = 0;
                else
                {
                    sumOfCalories += int.Parse(value);
                    maxCalories = Math.Max(maxCalories, sumOfCalories);
                }
                var msg = $"The current group of Elves carries {sumOfCalories} calories.\nCurrent max value is {maxCalories}";
                yield return output.Put(msg, _caloriesHoldByElves, sumOfCalories);
            }
            yield return output.Put(maxCalories.ToString(), _caloriesHoldByElves, sumOfCalories);
        }
        private static IEnumerable<int> Top3(List<int> sumOfCalories) => sumOfCalories.OrderByDescending(x => x).Take(3);
        public IEnumerable<PuzzleOutput> SolveSecondPart(string input)
        {
            ParseInput(input);
            int sumOfCalories = 0;
            var sumsOfCalories = new List<int>() { 0 };
            var output = new CalorieCountingOutputProvider();
            yield return output.Put("Start", _caloriesHoldByElves, sumOfCalories);
            foreach (var value in _caloriesHoldByElves)
            {
                if (value == 0)
                    sumsOfCalories.Add(0);
                else
                    sumsOfCalories[^1] += value;
                sumOfCalories = sumsOfCalories[^1]; // for the visualization
                var msg = "Top 3 of Elves groups holding the more calories:\n" + string.Join('\n', Top3(sumsOfCalories).Select(x => x.ToString()));
                yield return output.Put(msg,_caloriesHoldByElves, sumOfCalories);
            }
            yield return output.Put(
                Top3(sumsOfCalories).Sum().ToString(),
                _caloriesHoldByElves,
                0);
        }
    }
}
