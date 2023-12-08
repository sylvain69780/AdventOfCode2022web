using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CamelCards
{
    public class CamelCardsPart2Strategy : IPuzzleStrategy<CamelCardsModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(CamelCardsModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var s = model.Hands!
                .Select(x => (x.hand,besthand: x.hand.PossibleHands().OrderByDescending(y => y, new HandTypeComparer()).First(), x.bid))
                .OrderBy(x => x.besthand, new HandTypeComparer())
                .ThenBy(x=> x.hand, new HandValuesComparer("AKQT98765432J"))
                .Select((x,i) => x.bid*(i+1))
                .Sum();
            yield return updateContext();
            provideSolution(s.ToString());
        }
    }
}
