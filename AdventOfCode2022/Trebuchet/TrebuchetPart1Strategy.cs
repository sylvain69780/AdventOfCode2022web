using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Trebuchet
{
    public class TrebuchetPart1Strategy : IPuzzleStrategy<TrebuchetModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(TrebuchetModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var s = model.Input!;
            var r = 0;
            foreach (var l in s)
            {
                var x = l.Where(y => y >= '0' && y <= '9').ToArray();
                if (x.Length > 0)
                    r += int.Parse(string.Concat(x[0],x[^1]));
            }
            yield return updateContext();
            provideSolution(r.ToString());
        }
    }
}
