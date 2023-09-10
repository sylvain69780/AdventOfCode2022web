using System.Diagnostics;
using System.Text.RegularExpressions;
using AdventOfCode2022Solutions.PuzzleSolutions;

namespace AdventOfCode2022Solutions.PuzzleSolutions.MonkeyMath
{
    [Puzzle(21, "Monkey Math")]
    public class MonkeyMathSolution : IPuzzleSolution
    {
        private string _puzzleInput = string.Empty;
        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }
        private class JobOfEachMonkey
        {
            public Dictionary<string, (string Left, string Operator, string Right)>? ComputingMonkeys;
            public Dictionary<string, long>? NumberYellingMonkeys;
        }

        private static JobOfEachMonkey ReadPuzzleInput(string puzzleInput)
        {
            var input = puzzleInput.Split("\n");
            var nodeRegex = new Regex(@"([a-z]+): ([a-z]+) ([\+\-\/\*]) ([a-z]+)");
            var valueRegex = new Regex(@"([a-z]+): (\d+)");
            var computingMonkeys = input.Select(x => nodeRegex.Match(x))
                .Where(x => x.Success)
                .ToDictionary(x => x.Groups[1].Value, x => (Left: x.Groups[2].Value, Operator: x.Groups[3].Value, Right: x.Groups[4].Value));
            var valueHoldingMonkeys = input.Select(x => valueRegex.Match(x))
                .Where(x => x.Success)
                .ToDictionary(x => x.Groups[1].Value, x => long.Parse(x.Groups[2].Value));
            return new JobOfEachMonkey
            {
                ComputingMonkeys = computingMonkeys,
                NumberYellingMonkeys = valueHoldingMonkeys
            };
        }

        private long GetYelledNumber(JobOfEachMonkey jobOfEachMonkey, string monkeyName)
        {
            if (jobOfEachMonkey.NumberYellingMonkeys!.TryGetValue(monkeyName, out var number))
                return number;
            var (monkeyA, Operator, monkeyB) = jobOfEachMonkey.ComputingMonkeys![monkeyName];
            if (Operator == "+") return
                    GetYelledNumber(jobOfEachMonkey, monkeyA) + GetYelledNumber(jobOfEachMonkey, monkeyB);
            if (Operator == "-") return
                    GetYelledNumber(jobOfEachMonkey, monkeyA) - GetYelledNumber(jobOfEachMonkey, monkeyB);
            if (Operator == "*") return
                    GetYelledNumber(jobOfEachMonkey, monkeyA) * GetYelledNumber(jobOfEachMonkey, monkeyB);
            if (Operator == "/") return
                    GetYelledNumber(jobOfEachMonkey, monkeyA) / GetYelledNumber(jobOfEachMonkey, monkeyB);
            throw new NotImplementedException();
        }

        public string SolveFirstPart()
        {
            var jobOfEachMonkey = ReadPuzzleInput(_puzzleInput);
            return GetYelledNumber(jobOfEachMonkey, "root").ToString();
        }
        public string SolveSecondPart()
        {
            var jobOfEachMonkey = ReadPuzzleInput(_puzzleInput);
            var compute = (long guess) =>
            {
                jobOfEachMonkey.NumberYellingMonkeys!["humn"] = guess;
                var (monkeyA, _, monkeyB) = jobOfEachMonkey.ComputingMonkeys!["root"];
                return Math.Abs(GetYelledNumber(jobOfEachMonkey, monkeyB) - GetYelledNumber(jobOfEachMonkey, monkeyA));
            };

            var searchQueue = new PriorityQueue<(long Lower, long Upper), double>();
            var start = (Lower: 0L, Upper: long.MaxValue / 1000000);
            searchQueue.Enqueue(start, 0);
            var bestScore = long.MaxValue;
            var inputValuesGivingZero = new List<long>();
            while (searchQueue.TryDequeue(out var i, out _))
            {
                var score = compute(i.Lower);
                if (score < bestScore)
                {
                    bestScore = score;
                    Debug.WriteLine($"Best input {i.Lower} gives {score}");
                }
                if (score == 0)
                    inputValuesGivingZero.Add(i.Lower);
                var d = i.Upper - i.Lower;
                if (d == 0)
                    continue;
                double p = score / d;
                if (bestScore == 0 && p > 100)
                    break;
                if (d > 1)
                    searchQueue.Enqueue((i.Lower, i.Lower + d / 2), p);
                searchQueue.Enqueue((i.Lower + d / 2 + 1, i.Upper), p);
            }
            // there is several values that get 0 at the end !
            return inputValuesGivingZero.Min().ToString();
        }
    }
}