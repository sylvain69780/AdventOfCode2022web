using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RopeBridge
{
    public class RopeBridgeModel : IPuzzleModel
    {
        public IEnumerable<string>? SeriesOfMotions;
        public void Parse(string input)
        {
            SeriesOfMotions = input.Split("\n")
                .Select(x => x.Split(" "))
                .SelectMany(x => Enumerable.Range(0, int.Parse(x[1])), (x, y) => x[0]);
        }

        public static (int x, int y) MoveTailPosition((int x, int y) tail, (int x, int y) head)
        {
            var newTail = tail;
            var (dx, dy) = (head.x - newTail.x, head.y - newTail.y);
            if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1)
            {
                if (head.x - newTail.x > 0) newTail.x++;
                if (newTail.x - head.x > 0) newTail.x--;
                if (head.y - newTail.y > 0) newTail.y++;
                if (newTail.y - head.y > 0) newTail.y--;
            }
            return newTail;
        }

        public static readonly Dictionary<string, (int x, int y)> Directions = new()
                {
                    { "R", (1,0) },
                    { "L", (-1,0)},
                    { "U", (0,1) },
                    { "D", (0,-1)},
                };
    }
}
