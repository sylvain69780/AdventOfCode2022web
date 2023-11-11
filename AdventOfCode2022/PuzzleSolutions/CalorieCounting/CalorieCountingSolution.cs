using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting
{
    [PuzzleSolution(1)]
    public class CalorieCountingSolution : IPuzzleSolver
    {
        private List<int> _caloriesHoldByElves = new();
        private int _step;

        private CalorieCountingOutput ProvideResult(string output, int sum) => new()
        {
            Output = output,
            Step = _step,
            CurrentSum = sum,
            CaloriesHoldByElves = _caloriesHoldByElves
        };

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
            _step = 0;
        }
        public IEnumerable<PuzzleOutput> SolveFirstPart(string input)
        {
            ParseInput(input);
            int sumOfCalories = 0;
            int maxCalories = 0;
            foreach (var value in input.Split('\n'))
            {
                _step++;
                if (value == string.Empty)
                    sumOfCalories = 0;
                else
                {
                    sumOfCalories += int.Parse(value);
                    maxCalories = Math.Max(maxCalories, sumOfCalories);
                }
                yield return ProvideResult($"The current group of Elves carries {sumOfCalories} calories.\nCurrent max value is {maxCalories}", sumOfCalories);
            }
            yield return ProvideResult(maxCalories.ToString(), sumOfCalories);
        }
        private static IEnumerable<int> Top3(List<int> sumOfCalories) => sumOfCalories.OrderByDescending(x => x).Take(3);
        public IEnumerable<PuzzleOutput> SolveSecondPart(string input)
        {
            ParseInput(input);
            int sumOfCalories = 0;
            var sumsOfCalories = new List<int>() { 0 };
            foreach (var value in _caloriesHoldByElves)
            {
                _step++;
                if (value == 0)
                    sumsOfCalories.Add(0);
                else
                    sumsOfCalories[^1] += value;
                sumOfCalories = sumsOfCalories[^1]; // for the visualization
                yield return ProvideResult("Top 3 of Elves groups holding the more calories:\n" + string.Join('\n', Top3(sumsOfCalories).Select(x => x.ToString())), sumOfCalories);
            }
            yield return ProvideResult(Top3(sumsOfCalories).Sum().ToString(), 0);
        }
    }
}
