namespace AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting
{
    public class CalorieCountingOutputProvider
    {
        int _step = 0;
        public CalorieCountingOutput Put(string output, List<int> calories, int sum)
        {
            return new CalorieCountingOutput()
            {
                Step = _step++,
                Output = output,
                CaloriesHoldByElves = calories,
                CurrentSum = sum
            };
        }
    }
}
