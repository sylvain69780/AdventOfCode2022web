using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CamelCards
{
    public enum HandType
    {
        FiveOfAkind = 0,
        FourOfAKind = 1,
        FullHouse = 2,
        ThreeOfAKind = 3,
        TwoPair = 4,
        OnePair = 5,
        HighCard = 6
    }
}
