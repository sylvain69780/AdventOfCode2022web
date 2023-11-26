using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CathodeRayTube
{
    public class CathodeRayTubeModel : IPuzzleModel
    {
        public IEnumerable<int>? NumsToAdd { get; private set; }

        public void Parse(string input)
        {
            NumsToAdd = input
                .Split("\n")
                .Select(x => x.Split(" "))
                .SelectMany(x => x[0] == "noop" ? new int[] { 0 } : new int[] { 0, int.Parse(x[1]) });
        }
    }
}
