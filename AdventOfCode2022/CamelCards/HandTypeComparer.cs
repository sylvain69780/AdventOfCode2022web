using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CamelCards
{
    internal class HandTypeComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            var (xt, yt) = (x!.GetHandType(),y!.GetHandType());
            if ( xt < yt )
                return 1;
            if (xt > yt)
                return -1;
            return 0;
        }
    }
}
