namespace sylvain69780.AdventOfCode2022.Domain.CalorieCounting
{
    public interface ICalorieCountingStrategy
    {
        public IEnumerable<ProgressInfo> GetStepsToSolution(IEnumerable<int> caloriesHoldByElves, Func<int, ProgressInfo> progressInfo, Action<string> provideSolution);
    }
}
