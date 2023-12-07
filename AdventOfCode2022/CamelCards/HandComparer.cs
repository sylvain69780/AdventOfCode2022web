using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CamelCards
{
    internal class HandComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            var (xt, yt) = (x!.GetHandType(),y!.GetHandType());
            if ( xt < yt )
                return 1;
            if (xt > yt)
                return -1;
            for( var i = 0; i<x!.Length; i++)
            {
                var (ix, iy) = (CardsTypes.IndexOf(x[i]), CardsTypes.IndexOf(y[i]));
                if (ix < iy)
                    return 1;
                if (ix > iy)
                    return -1;
            }
            return 0;
        }

        public static readonly string CardsTypes = "AKQJT98765432";

    }
}
