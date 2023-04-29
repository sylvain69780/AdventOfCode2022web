namespace AdventOfCode2022web.Domain.Puzzle
{
    public class CalorieCounting : PuzzleSolver
    {
        private static string[] ToLines(string s) => s.Split("\n");
        protected override string Part1(string puzzleInput)
        {
            int elfCalories = 0, maxCalories = 0;
            foreach (var value in ToLines(puzzleInput))
            {
                elfCalories = value == string.Empty ? 0 : elfCalories + int.Parse(value);
                maxCalories = Math.Max(maxCalories, elfCalories);
            }
            return maxCalories.ToString();
        }
        protected override string Part2(string puzzleInput)
        {
            var calories = new List<int>() { 0 };
            foreach (var value in ToLines(puzzleInput))
            {
                if (value == string.Empty)
                    calories.Add(0);
                else
                    calories[-1] += int.Parse(value);
            }
            return calories.OrderByDescending(x => x).Take(3).Sum().ToString();
        }
    }
}
