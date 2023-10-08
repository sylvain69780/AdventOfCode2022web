namespace AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting
{
    public class CalorieCountingSolution : IPuzzleSolutionIter
    {
        private static string[] ToLines(string s) => s.Split("\n");

        private string[]? _puzzleInput;

        public string[]? GroupsOfElves => _puzzleInput;

        private int sumOfCalories;
        public int CurrentSum => sumOfCalories;

        public void Initialize(string input)
        {
            _puzzleInput = ToLines(input);
        }

        public IEnumerable<string> SolveFirstPart()
        {
            sumOfCalories = 0;
                int maxCalories = 0;
            foreach (var value in _puzzleInput!)
            {
                if (value == string.Empty)
                    sumOfCalories = 0;
                else
                {
                    sumOfCalories += int.Parse(value);
                    maxCalories = Math.Max(maxCalories, sumOfCalories);
                }
                yield return $"The current group of Elves carries {sumOfCalories} calories.\nCurrent max value is {maxCalories}";
            }
            yield return maxCalories.ToString();
        }
        public IEnumerable<string> SolveSecondPart()
        {
            sumOfCalories = 0;
            var sumsOfCalories = new List<int>() { 0 };
            foreach (var value in _puzzleInput!)
            {
                if (value == string.Empty)
                    sumsOfCalories.Add(0);
                else
                    sumsOfCalories[^1] += int.Parse(value);
                sumOfCalories = sumsOfCalories[^1]; // for the visualization
                yield return "Top 3 of Elves groups holding the more calories:\n"
                    + string.Join('\n', Top3(sumsOfCalories).Select(x => x.ToString()));
            }
            yield return Top3(sumsOfCalories).Sum().ToString();

            static IEnumerable<int> Top3(List<int> sumOfCalories) => sumOfCalories.OrderByDescending(x => x).Take(3);
        }
    }
}
