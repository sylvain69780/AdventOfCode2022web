using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CamelCards
{
    internal class HandJokerFirstComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException("Input strings cannot be null");
            for ( var i = 0; i<x.Length; i++)
            {
                var (ix, iy) = (CardsTypes.IndexOf(x[i]), CardsTypes.IndexOf(y[i]));
                if (ix < iy)
                    return 1;
                if (ix > iy)
                    return -1;
            }
            return 0;
        }
        static readonly string CardsTypes = "AKQJT98765432";
    }
}
