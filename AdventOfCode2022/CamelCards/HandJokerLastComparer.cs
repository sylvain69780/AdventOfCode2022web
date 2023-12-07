using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CamelCards
{
    internal class HandJokerLastComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            for (var i = 0; i < x!.Length; i++)
            {
                var (ix, iy) = (_cardsTypes.IndexOf(x[i]), _cardsTypes.IndexOf(y[i]));
                if (ix < iy)
                    return 1;
                if (ix > iy)
                    return -1;
            }
            return 0;
        }

        static readonly string _cardsTypes = "AKQT98765432J";
    }
}
