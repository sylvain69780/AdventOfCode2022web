namespace AdventOfCode2022web.Domain.Puzzle
{
    public class CalorieCounting : PuzzleSolver
    {
        protected override string Part1(string inp)
        {
            var elfCalories = 0;
            var maxCalories = 0;
            foreach (var line in inp.Split("\n").Append(string.Empty))
            {
                if (line != string.Empty)
                    elfCalories += int.Parse(line);
                else
                {
                    if (elfCalories > maxCalories)
                        maxCalories = elfCalories;
                    elfCalories = 0;
                }
            }
            return maxCalories.ToString();
        }
        protected override string Part2(string inp)
        {
            var calories = new List<int>();
            var elfCalories = 0;
            foreach (var line in inp.Split("\n").Append(string.Empty))
            {
                if (line != string.Empty)
                    elfCalories += int.Parse(line);
                else
                {
                    calories.Add(elfCalories);
                    elfCalories = 0;
                }
            }
            return calories.OrderByDescending(x => x).Take(3).Sum().ToString();
        }
    }
}