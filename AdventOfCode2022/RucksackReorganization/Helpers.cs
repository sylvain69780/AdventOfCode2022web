using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RucksackReorganization
{

    internal class Helpers
    {
        public static int Priority(char item) => item >= 'a' ? item - 'a' + 1 : item - 'A' + 27;

    }
}
