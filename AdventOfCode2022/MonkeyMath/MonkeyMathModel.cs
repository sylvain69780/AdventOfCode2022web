using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.MonkeyMath
{
    public class MonkeyMathModel : IPuzzleModel
    {
        private JobOfEachMonkey? _jobOfEachMonkey;
        public JobOfEachMonkey? JobOfEachMonkey => _jobOfEachMonkey;
        public void Parse(string input)
        {
            _jobOfEachMonkey = ReadPuzzleInput(input);
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

        public static long GetYelledNumber(JobOfEachMonkey jobOfEachMonkey, string monkeyName)
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

    }
}
