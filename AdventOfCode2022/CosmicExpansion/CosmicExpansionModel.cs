using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CosmicExpansion
{
    public class CosmicExpansionModel : IPuzzleModel
    {
        List<(int id, long x, long y)>? _galaxies;
        public List<(int id,long x, long y)>? Galaxies => _galaxies;
        public void Parse(string input)
        {
            var _input = input.Replace("\r", "").Split("\n");
            _galaxies = _input
                            .SelectMany((l, y) => l.Select((c, x) => (c, x, y)))
                            .Where(g => g.c == '#')
                            .Select((g,id) => (id,(long)g.x,(long) g.y))
                            .ToList();
        }
    }
}
