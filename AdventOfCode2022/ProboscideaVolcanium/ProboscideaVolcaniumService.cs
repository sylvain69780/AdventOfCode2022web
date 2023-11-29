using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProboscideaVolcanium
{
    public class ProboscideaVolcaniumService : SimplePuzzleService<ProboscideaVolcaniumModel>
    {
        public ProboscideaVolcaniumService(IEnumerable<IPuzzleStrategy<ProboscideaVolcaniumModel>> strategies) : base(strategies)
        {
        }
    }
}
