namespace Domain.RucksackReorganization
{
    public abstract class RucksackReorganizationStrategyBase : IPuzzleStrategy<RucksackReorganizationModel>
    {
        abstract public string Name { get; set; }

        abstract public IEnumerable<ProcessingProgressModel> GetSteps(RucksackReorganizationModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution);

        public static int Priority(char item) => item >= 'a' ? item - 'a' + 1 : item - 'A' + 27;
    }
}