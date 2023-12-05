using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Scratchcards
{
    public class ScratchcardsModel : IPuzzleModel
    {
        List<(int[] winningNumbers, int[] numbersYouHave)> _cards = new();
        public List<(int[] winningNumbers, int[] numbersYouHave)> Cards => _cards;
        public void Parse(string input)
        {
            var lines = input.Replace("\r", "").Split("\n");
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Replace("  ", " ");
                var part = line.Split(": ")[1];
                var part2 = part.Split(" | ");
                _cards.Add(
                    (part2[0].Split(' ').Select(x => int.Parse(x)).ToArray(), part2[1].Split(' ').Select(x => int.Parse(x)).ToArray())
                    );
            }
        }
    }
}
