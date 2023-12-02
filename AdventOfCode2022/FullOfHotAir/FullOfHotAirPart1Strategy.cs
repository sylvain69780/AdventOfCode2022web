using Domain.ProboscideaVolcanium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FullOfHotAir
{
    public class FullOfHotAirPart1Strategy : IPuzzleStrategy<FullOfHotAirModel>
    {
        public string Name { get; set; } = "Part 1";

        private static readonly char[] values = new char[] { '=', '-', '0', '1', '2' };

        public IEnumerable<ProcessingProgressModel> GetSteps(FullOfHotAirModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var result = 0L;
            foreach (var line in model.Input!)
            {
                var b = 1L;
                var res = 0L;
                foreach (var c in line.Reverse())
                {
                    var v = (long)Array.IndexOf(values, c) - 2;
                    res += v * b;
                    b *= 5;
                }
                result += res;
            }
            Console.WriteLine(result);
            var snafu = new Stack<char>();
            var num = result;
            while (num != 0)
            {
                var rem = num % 5L;
                snafu.Push(values[(rem + 2) % 5]);
                var addUp = rem > 2 ? 1 : 0;
                num /= 5L;
                num += addUp;
            }
            yield return updateContext();
            provideSolution(string.Concat(snafu));
        }
    }
}
