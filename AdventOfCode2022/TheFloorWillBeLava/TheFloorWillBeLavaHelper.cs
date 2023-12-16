using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TheFloorWillBeLava
{
    internal static class TheFloorWillBeLavaHelper
    {
        public static char GetTile(this string[] map,int x, int y)
        {
            if (x >= map[0].Length || x < 0 || y >= map.Length || y < 0)
                return '#';
            else
                return map[y][x];
        }
        public static string[] Visu(this int[,] energyMap)
        {
            var res = new string[energyMap.GetLength(1)];
            var sb = new StringBuilder();
            for (var y = 0; y < energyMap.GetLength(1); y++)
            {
                sb.Clear();
                for (var x = 0; x < energyMap.GetLength(0); x++)
                    if (energyMap[x, y] == 1)
                        sb.Append('#');
                    else
                        sb.Append('.');
                res[y] = sb.ToString();
            }
            return res;
        }
    }
}
