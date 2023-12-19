using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LavaductLagoon
{
    public class LavaductLagoonModel : IPuzzleModel
    {
        (char dir, int count, string rgb)[] _digPlan;
        public (char dir, int count, string rgb)[] DigPlan => _digPlan;
        public void Parse(string input)
        {
            _digPlan = input.Replace("\r", "").Split("\n").Select(x => x.Split(" "))
                .Select(x => (dir: x[0][0], count: int.Parse(x[1]), rgb: x[2])).ToArray();
        }
    }
}
