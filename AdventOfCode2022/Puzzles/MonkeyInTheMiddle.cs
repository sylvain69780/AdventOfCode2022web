﻿using System.Diagnostics;
using System.Text;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(11, "Monkey In The Middle")]
    public class MonkeyInTheMiddle : IPuzzleSolver
    {
        class Monkey
        {
            public int Id;
            public List<long> WorryLevelOfItems = new();
            public char OperationToPerform;
            public int? ValueToAddOrMultiply;
            public long DivisibilityToTest;
            public int MonkeyRecipientIfDivisible;
            public int MonkeyRecipientIfNotDivisible;
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
                var worryLevelOfItems = input.Current[18..].Split(',').Select(x => long.Parse(x)).ToList();
                input.MoveNext();
                var operAndValue = input.Current[19..].Split(' ');
                input.MoveNext();
                var divisibilityToTest = int.Parse(input.Current[21..]);
                input.MoveNext();
                var ifTrue = int.Parse(input.Current[29..]);
                input.MoveNext();
                var ifFalse = int.Parse(input.Current[29..]);
                input.MoveNext();
                monkeys.Add(new Monkey
                {
                    Id = counter++,
                    WorryLevelOfItems = worryLevelOfItems,
                    OperationToPerform = operAndValue[1][0],
                    ValueToAddOrMultiply = operAndValue[2] == "old" ? null : int.Parse(operAndValue[2]),
                    DivisibilityToTest = divisibilityToTest,
                    MonkeyRecipientIfDivisible = ifTrue,
                    MonkeyRecipientIfNotDivisible = ifFalse
                });
            }
            return monkeys;
        }

        private static string Visualize(List<Monkey> monkeys, int round)
        {
            var sb = new StringBuilder($"After round {round} the monkeys are holding items with these worry levels:\n");
            foreach (var monkey in monkeys)
                sb.Append($"Monkey {monkey.Id}: {string.Join(", ", monkey.WorryLevelOfItems)}\n");
            return sb.Append(monkeys.Select(x => x.Inspections).OrderByDescending(x => x).Take(2).Aggregate(1L, (x, y) => y * x))
                .ToString();
        }

        public string SolveFirstPart(string puzzleInput)
        {
            var monkeys = BuildMonkeyList(puzzleInput);
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
            return Visualize(monkeys, maxRound);
        }
        public string SolveSecondPart(string puzzleInput)
        {
            var monkeys = BuildMonkeyList(puzzleInput);
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
                    //yield return Visualize(monkeys, round);
                    stopwatch.Restart();
                }
            }
            return Visualize(monkeys, maxRound);
        }
    }
}