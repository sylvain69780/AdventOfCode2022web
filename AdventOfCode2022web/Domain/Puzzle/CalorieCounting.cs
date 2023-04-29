namespace AdventOfCode2022web.Domain.Puzzle
{
    public class CalorieCounting : PuzzleSolver
    {
        private static string[] ToLines(string s) => s.Split("\n");
        protected override string Part1(string puzzleInput)
        {
            int sumOfCalories = 0, maxCalories = 0;
            foreach (var value in ToLines(puzzleInput))
            {
                sumOfCalories = value == string.Empty ? 0 : sumOfCalories + int.Parse(value);
                maxCalories = Math.Max(maxCalories, sumOfCalories);
            }
            return maxCalories.ToString();
        }
        protected override string Part2(string puzzleInput)
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
