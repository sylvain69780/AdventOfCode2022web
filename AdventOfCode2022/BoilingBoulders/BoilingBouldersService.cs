using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BoilingBoulders
{
    public class BoilingBouldersService : SimplePuzzleService<BoilingBouldersModel>
    {
        public BoilingBouldersService(IEnumerable<IPuzzleStrategy<BoilingBouldersModel>> strategies) : base(strategies)
        {
        }
    }
}
