using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FullOfHotAir
{
    public class FullOfHotAirService : SimplePuzzleService<FullOfHotAirModel>
    {
        public FullOfHotAirService(IEnumerable<IPuzzleStrategy<FullOfHotAirModel>> strategies) : base(strategies)
        {
        }
    }
}
