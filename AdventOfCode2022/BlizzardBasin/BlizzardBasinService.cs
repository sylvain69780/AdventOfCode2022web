using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BlizzardBasin
{
    public class BlizzardBasinService : SimplePuzzleService<BlizzardBasinModel>
    {
        public BlizzardBasinService(IEnumerable<IPuzzleStrategy<BlizzardBasinModel>> strategies) : base(strategies)
        {
        }
    }
}
