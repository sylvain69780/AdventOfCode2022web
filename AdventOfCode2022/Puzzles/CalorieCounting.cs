namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(1, "Calorie Counting")]
    public class CalorieCounting : IPuzzleSolver
    {
        private static string[] ToLines(string s) => s.Split("\n");
        public string SolveFirstPart(string puzzleInput)
        {
            int sumOfCalories = 0, maxCalories = 0;
            foreach (var value in ToLines(puzzleInput))
            {
                if (value == string.Empty)
                    sumOfCalories = 0;
                else
                    sumOfCalories += int.Parse(value);
                maxCalories = Math.Max(maxCalories, sumOfCalories);
            }
             return maxCalories.ToString();
        }
        public string SolveSecondPart(string puzzleInput)
        {
            var sumOfCalories = new List<int>() { 0 };
            foreach (var value in ToLines(puzzleInput))
            {
                if (value == string.Empty)
                    sumOfCalories.Add(0);
                else
                    sumOfCalories[^1] += int.Parse(value);
            }
             return sumOfCalories.OrderByDescending(x => x).Take(3).Sum().ToString();
        }
    }
}
