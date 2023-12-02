using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Trebuchet
{
    public class TrebuchetPart2Strategy : IPuzzleStrategy<TrebuchetModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(TrebuchetModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var digits = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var s = model.Input;
            var r = 0;
            foreach (var l in s!)
            {
                var digitsFound = new List<int>();
                for (var i = 0; i < l.Length; i++)
                {
                    if (l[i] >= '0' && l[i] <= '9')
                        digitsFound.Add(l[i] - '0');
                    else
                        for (var d = 0; d < digits.Count; d++)
                            if (l.Length - i >= digits[d].Length && l.Substring(i, digits[d].Length) == digits[d])
                            // if (l[i..] == digits[d])
                            {
                                digitsFound.Add(d + 1);
                                break;
                            }
                }
                r += int.Parse(string.Concat(digitsFound[0], digitsFound[^1]));
            }
            yield return updateContext();
            provideSolution(r.ToString());
        }
    }
}
