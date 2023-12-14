using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PointOfIIcidence
{
    public class PointOfIIcidenceModel : IPuzzleModel
    {
        public (int width, int height, string[] map)[]? Patterns => _patterns;
        (int width,int height, string[] map)[]? _patterns;
        public void Parse(string input)
        {
            var inp = input.Replace("\r", "").Split("\n\n");
            _patterns = inp.Select(x => x.Split("\n")).Select(x => (width: x[0].Length,height:x.Length,map:x)).ToArray();
        }
    }
}
