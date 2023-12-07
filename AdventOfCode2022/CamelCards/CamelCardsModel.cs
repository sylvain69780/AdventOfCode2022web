using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CamelCards
{
    public class CamelCardsModel : IPuzzleModel
    {
        List<(string hand, long bid)>? _hands;
        public List<(string hand, long bid)>? Hands => _hands;

        public void Parse(string input)
        {
            _hands = input.Replace("\r", "").Split("\n")
                .Select(x => x.Split(" "))
                .Select(x => (x[0], long.Parse(x[1])))
                .ToList();
        }
    }
}
