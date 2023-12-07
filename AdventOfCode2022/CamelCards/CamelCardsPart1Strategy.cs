using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CamelCards
{
    public class CamelCardsPart1Strategy : IPuzzleStrategy<CamelCardsModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(CamelCardsModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var hands = model.Hands!;
            var r = hands.OrderBy(x => x.hand, new HandComparer()).ToArray();
            var s = 0L;
            for ( var i = 0; i<r.Length; i++)
            {
                s += (i+1) * r[i].bid;
            }
            yield return updateContext();
            provideSolution(s.ToString());
        }
    }
}
