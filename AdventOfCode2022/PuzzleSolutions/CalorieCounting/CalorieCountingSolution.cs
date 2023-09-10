using AdventOfCode2022Solutions.PuzzleSolutions;

namespace AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting
{
    [Puzzle(1, "Calorie Counting")]
    public class CalorieCountingSolution : IPuzzleSolutionIter
    {
        private static string[] ToLines(string s) => s.Split("\n");

        private string[]? _puzzleInput;

        public void Initialize(string input)
        {
            _puzzleInput = ToLines(input);
        }

        public IEnumerable<string> SolveFirstPart()
        {
            int sumOfCalories = 0, maxCalories = 0;
            foreach (var value in _puzzleInput!)
            {
                if (value == string.Empty)
                    sumOfCalories = 0;
                else
                {
                    sumOfCalories += int.Parse(value);
                    maxCalories = Math.Max(maxCalories, sumOfCalories);
                    yield return $"The current group of Elves carries {sumOfCalories} calories.\nCurrent max value is {maxCalories}";
                }
            }
            yield return maxCalories.ToString();
        }
        public IEnumerable<string> SolveSecondPart()
        {
            var sumOfCalories = new List<int>() { 0 };
            foreach (var value in _puzzleInput!)
            {
                if (value == string.Empty)
                    sumOfCalories.Add(0);
                else
                    sumOfCalories[^1] += int.Parse(value);
                yield return "Top 3 of Elves groups holding the more calories:\n"
                    + string.Join('\n', Top3(sumOfCalories).Select(x => x.ToString()));
            }
            yield return Top3(sumOfCalories).Sum().ToString();

            static IEnumerable<int> Top3(List<int> sumOfCalories) => sumOfCalories.OrderByDescending(x => x).Take(3);
        }
    }
}
