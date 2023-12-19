using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LavaductLagoon
{
    public class LavaductLagoonService : SimplePuzzleService<LavaductLagoonModel>
    {
        public LavaductLagoonService(IEnumerable<IPuzzleStrategy<LavaductLagoonModel>> strategies) : base(strategies)
        {
        }
    }
}
