using System.Diagnostics;

namespace Domain.MonkeyInTheMiddle
{
    public class MonkeyInTheMiddlePart2Strategy : IPuzzleStrategy<MonkeyInTheMiddleModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(MonkeyInTheMiddleModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var monkeys = model.Monkeys;
            var bigDiv = monkeys.Select(x => x.DivisibilityToTest).Aggregate(1L, (x, y) => y * x);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            const int maxRound = 10000;
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
                        newWorryLevelOfItem %= bigDiv;
                        if (newWorryLevelOfItem % monkey.DivisibilityToTest == 0)
                            monkeys[monkey.MonkeyRecipientIfDivisible].WorryLevelOfItems.Add(newWorryLevelOfItem);
                        else
                            monkeys[monkey.MonkeyRecipientIfNotDivisible].WorryLevelOfItems.Add(newWorryLevelOfItem);
                    }
                    monkey.WorryLevelOfItems.Clear();
                }
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    stopwatch.Restart();
                }
            }
            yield return updateContext();
            provideSolution(monkeys.Select(x => x.Inspections).OrderByDescending(x => x).Take(2).Aggregate(1L, (x, y) => y * x).ToString());
        }
    }
}
