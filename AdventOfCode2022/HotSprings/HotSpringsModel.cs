using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HotSprings
{
    public class HotSpringsModel : IPuzzleModel
    {
        (string springs, long[] ranges)[]? _rows;
        public (string springs, long[] ranges)[]? Rows => _rows;

        public Dictionary<(int i, int groups), long>? Cache;
        public void Parse(string input)
        {
            // operational (.) or damaged (#)
            _rows = input.Replace("\r", "").Split("\n")
                            .Select(x => x.Split(" "))
                            .Select(x => (springs: x[0], ranges: x[1].Split(',').Select(x => long.Parse(x)).ToArray()))
                            .ToArray();
        }
    }
}
