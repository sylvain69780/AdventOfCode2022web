using System.Diagnostics;
using System.Security.Cryptography;

namespace AdventOfCode2022web.Domain.Puzzle
{
    public class MonkeyInTheMiddle : IPuzzleSolver
    {
        class Monkey
        {
            public int Id;
            public List<long> WorryLevelOfItems = new ();
            public char OperationToPerform;
            public int? ValueToAddOrMultiply;
            public long Test;
            public int IfTrue;
            public int IfFalse;
            public long Inspections;
        }

        private static List<Monkey> BuildMonkeyList(string puzzleInput)
        {
            var input = puzzleInput.Split("\n").AsEnumerable().GetEnumerator();
            var monkeys = new List<Monkey>();
            var counter = 0;
            while (input.MoveNext())
            {
                input.MoveNext();
                var items = input.Current[18..].Split(',').Select(x => long.Parse(x)).ToList();
                input.MoveNext();
                var split = input.Current[19..].Split(' ');
                input.MoveNext();
                var test = int.Parse(input.Current[21..]);
                input.MoveNext();
                var ifTrue = int.Parse(input.Current[29..]);
                input.MoveNext();
                var ifFalse = int.Parse(input.Current[29..]);
                input.MoveNext();
                monkeys.Add(new Monkey
                {
                    Id = counter++,
                    WorryLevelOfItems = items,
                    OperationToPerform = split[1][0],
                    ValueToAddOrMultiply = split[2] == "old" ? null : int.Parse(split[2]),
                    Test = test,
                    IfFalse = ifFalse,
                    IfTrue = ifTrue
                });
            }
            return monkeys;
        }

        private static string Visualize(List<Monkey> monkeys)
        {
            return monkeys.Select(x => x.Inspections).OrderByDescending(x => x).Take(2).Aggregate(1L, (x, y) => y * x).ToString();
        }

        public IEnumerable<string> SolveFirstPart(string puzzleInput)
        {
            var monkeys = BuildMonkeyList(puzzleInput);
            foreach (var round in Enumerable.Range(1, 20))
            {
                foreach (var monkey in monkeys)
                {
                    monkey.Inspections += monkey.WorryLevelOfItems.Count;
                    foreach (var currentWorryLevelOfItem in monkey.WorryLevelOfItems)
                    {
                        var newWorryLevelOfItem = currentWorryLevelOfItem;
                        if (monkey.OperationToPerform == '*')
                            newWorryLevelOfItem *= monkey.ValueToAddOrMultiply ?? currentWorryLevelOfItem;
                        else
                            newWorryLevelOfItem += monkey.ValueToAddOrMultiply ?? currentWorryLevelOfItem;
                        newWorryLevelOfItem /= 3;
                        if (newWorryLevelOfItem % monkey.Test == 0)
                            monkeys[monkey.IfTrue].WorryLevelOfItems.Add(newWorryLevelOfItem);
                        else
                            monkeys[monkey.IfFalse].WorryLevelOfItems.Add(newWorryLevelOfItem);
                    }
                    monkey.WorryLevelOfItems.Clear();
                }
            }
            yield return Visualize(monkeys);
        }
        public IEnumerable<string> SolveSecondPart(string puzzleInput)
        {
            var monkeys = BuildMonkeyList(puzzleInput);
            var bigDiv = monkeys.Select(x => x.Test).Aggregate(1L, (x, y) => y * x);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var round in Enumerable.Range(1, 10000))
            {
                foreach (var monkey in monkeys)
                {
                    monkey.Inspections += monkey.WorryLevelOfItems.Count;
                    foreach (var currentWorryLevelOfItem in monkey.WorryLevelOfItems)
                    {
                        var newWorryLevelOfItem = currentWorryLevelOfItem;
                        if (monkey.OperationToPerform == '*')
                            newWorryLevelOfItem *= monkey.ValueToAddOrMultiply ?? currentWorryLevelOfItem;
                        else
                            newWorryLevelOfItem += monkey.ValueToAddOrMultiply ?? currentWorryLevelOfItem;
                        newWorryLevelOfItem %= bigDiv;
                        if (newWorryLevelOfItem % monkey.Test == 0)
                            monkeys[monkey.IfTrue].WorryLevelOfItems.Add(newWorryLevelOfItem);
                        else
                            monkeys[monkey.IfFalse].WorryLevelOfItems.Add(newWorryLevelOfItem);
                    }
                    monkey.WorryLevelOfItems.Clear();
                }
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    yield return Visualize(monkeys);
                    stopwatch.Restart();
                }
            }
            yield return Visualize(monkeys);
        }
    }
}