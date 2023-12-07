using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CamelCards
{
    internal class HandComparerJocker : IComparer<(string hand, string besthand) >
    {
        public int Compare((string hand,string besthand) x, (string hand, string besthand) y)
        {
            var (xt, yt) = (GetCardType(x.besthand), GetCardType(y.besthand));
            if ( xt < yt )
                return 1;
            if (xt > yt)
                return -1;
            for( var i = 0; i<x.hand.Length; i++)
            {
                var (ix, iy) = (CardsTypes.IndexOf(x.hand[i]), CardsTypes.IndexOf(y.hand[i]));
                if (ix < iy)
                    return 1;
                if (ix > iy)
                    return -1;
            }
            return 0;
        }

        public static HandType GetCardType(string hand)
        {
            var g = hand.GroupBy(x => x).Select(x => (Card: x.Key, Count: x.Count())).OrderByDescending(x => x.Count).ToArray();
            if (g[0].Count == 5)
                return HandType.FiveOfAkind;
            if (g[0].Count == 4)
                return HandType.FourOfAKind;
            if (g[0].Count == 3 && g[1].Count == 2)
                return HandType.FullHouse;
            if (g[0].Count == 3)
                return HandType.ThreeOfAKind;
            if (g[0].Count == 2 && g[1].Count == 2)
                return HandType.TwoPair;
            if (g[0].Count == 2)
                return HandType.OnePair;
            return HandType.HighCard;
        }

        public static readonly string CardsTypes = "AKQT98765432J";

    }
}
