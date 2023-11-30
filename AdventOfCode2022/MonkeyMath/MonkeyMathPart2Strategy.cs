using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MonkeyMath
{
    public class MonkeyMathPart2Strategy : IPuzzleStrategy<MonkeyMathModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(MonkeyMathModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var compute = (long guess) =>
            {
                model.JobOfEachMonkey!.NumberYellingMonkeys!["humn"] = guess;
                var (monkeyA, _, monkeyB) = model.JobOfEachMonkey.ComputingMonkeys!["root"];
                return Math.Abs(MonkeyMathModel.GetYelledNumber(model.JobOfEachMonkey, monkeyB) - MonkeyMathModel.GetYelledNumber(model.JobOfEachMonkey, monkeyA));
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
            yield return updateContext();
            provideSolution(inputValuesGivingZero.Min().ToString());
        }
    }
}
