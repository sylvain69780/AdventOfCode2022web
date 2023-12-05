using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Scratchcards
{
    public class ScratchcardsPart2Strategy : IPuzzleStrategy<ScratchcardsModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(ScratchcardsModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var cards = model.Cards!;
            var copies = new int[cards.Count];
            Array.Fill(copies, 1);
            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var score = card.numbersYouHave.Where(x => card.winningNumbers.Any(y => x == y)).Count();
                for (var j = 1; j <= Math.Min(score, cards.Count-1-i); j++)
                    copies[i + j]+=copies[i];
            }
            yield return updateContext();
            provideSolution(copies.Sum().ToString());
        }
    }
}
