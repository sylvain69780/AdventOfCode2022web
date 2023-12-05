using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Scratchcards
{
    public class ScratchcardsPart1Strategy : IPuzzleStrategy<ScratchcardsModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(ScratchcardsModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var sum = 0;
            foreach (var card in model.Cards!)
            {
                var score = card.numbersYouHave.Where(x => card.winningNumbers.Any(y => x == y))
                   .Count();
                if ( score > 0)
                    sum += 1<<(score-1);
            }
            yield return updateContext();
            provideSolution(sum.ToString());
        }
    }
}
