namespace Domain.MonkeyInTheMiddle
{
    public class MonkeyInTheMiddlePart1Strategy : IPuzzleStrategy<MonkeyInTheMiddleModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(MonkeyInTheMiddleModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var monkeys = model.Monkeys;
            const int maxRound = 20;
            foreach (var round in Enumerable.Range(1, maxRound))
            {
                foreach (var monkey in monkeys)
                {
                    monkey.Inspections += monkey.WorryLevelOfItems.Count;
                    foreach (var currentWorryLevelOfItem in monkey.WorryLevelOfItems)
                    {
                        var newWorryLevelOfItem = currentWorryLevelOfItem;
                        var valueToAddOrMultiply = monkey.ValueToAddOrMultiply ?? currentWorryLevelOfItem;
                        if (monkey.OperationToPerform == '*')
                            newWorryLevelOfItem *= valueToAddOrMultiply;
                        else
                            newWorryLevelOfItem += valueToAddOrMultiply;
                        newWorryLevelOfItem /= 3;
                        if (newWorryLevelOfItem % monkey.DivisibilityToTest == 0)
                            monkeys[monkey.MonkeyRecipientIfDivisible].WorryLevelOfItems.Add(newWorryLevelOfItem);
                        else
                            monkeys[monkey.MonkeyRecipientIfNotDivisible].WorryLevelOfItems.Add(newWorryLevelOfItem);
                    }
                    monkey.WorryLevelOfItems.Clear();
                }
            }
            yield return updateContext();
            provideSolution(monkeys.Select(x => x.Inspections).OrderByDescending(x => x).Take(2).Aggregate(1L, (x, y) => y * x).ToString());
        }
    }
}
