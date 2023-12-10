using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PipeMaze
{
    internal static class TileHelper
    {
        public static bool openRight(this char c )
        {
            if (c == '-')
                return true;
            if (c == 'L')
                return true;
            if (c == 'F')
                return true;
            return false;
        }
    }
}
