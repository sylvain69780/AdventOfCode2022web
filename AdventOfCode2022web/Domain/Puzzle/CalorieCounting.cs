namespace AdventOfCode2022web.Domain.Puzzle
{
    public class CalorieCounting : IPuzzleSolver
    {
        public async IAsyncEnumerable<string> Part1Async(string input)
        {
            Input = input;
            yield return Part1();
            await Task.Delay(1);
        }
        public async IAsyncEnumerable<string> Part2Async(string input)
        {
            Input = input;
            yield return Part2();
            await Task.Delay(1);
        }
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            var elfCalories = 0;
            var maxCalories = 0;
            foreach (var line in Input.Split("\n").Append(string.Empty))
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
        public string Part2()
        {
            var calories = new List<int>();
            var elfCalories = 0;
            foreach (var line in Input.Split("\n").Append(string.Empty))
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