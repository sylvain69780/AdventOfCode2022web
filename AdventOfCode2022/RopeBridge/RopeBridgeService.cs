using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RopeBridge
{
    public class RopeBridgeService : SimplePuzzleService<RopeBridgeModel>
    {
        public RopeBridgeService(IEnumerable<IPuzzleStrategy<RopeBridgeModel>> strategies) : base(strategies)
        {
        }
    }
}
