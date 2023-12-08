using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CamelCards
{
    internal class HandValuesComparer : IComparer<string>
    {
        public HandValuesComparer(string values)
        {
            _cardsValues = values;
        }
        public int Compare(string? x, string? y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException("Input strings cannot be null");
            for ( var i = 0; i<x.Length; i++)
            {
                var (ix, iy) = (_cardsValues.IndexOf(x[i]), _cardsValues.IndexOf(y[i]));
                if (ix < iy)
                    return 1;
                if (ix > iy)
                    return -1;
            }
            return 0;
        }
        readonly string _cardsValues;
    }
}
