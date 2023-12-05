using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.GearRatios
{
    public class GearRatiosService : SimplePuzzleService<GearRatiosModel>
    {
        public GearRatiosService(IEnumerable<IPuzzleStrategy<GearRatiosModel>> strategies) : base(strategies)
        {
        }
    }
}
