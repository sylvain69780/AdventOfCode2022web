namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(1, "Calorie Counting")]
    public class CalorieCounting : IPuzzleSolutionIter
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
                    +string.Join('\n',sumOfCalories.OrderByDescending(x => x).Take(3).Select(x => x.ToString()));
            }
            yield return sumOfCalories.OrderByDescending(x => x).Take(3).Sum().ToString();
        }
    }
}
