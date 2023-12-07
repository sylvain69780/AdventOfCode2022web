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
            var s = model.Hands!
                .OrderBy(x => x.hand, new HandTypeComparer())
                .ThenBy(x => x.hand,new HandJokerFirstComparer())
                .Select((x, i) => x.bid * (i + 1)).Sum();
            yield return updateContext();
            provideSolution(s.ToString());
        }
    }
}
