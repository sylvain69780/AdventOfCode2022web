using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SupplyStacks
{
    public class SupplyStacksService : SimplePuzzleService<SupplyStacksModel>
    {
        public SupplyStacksService(IEnumerable<IPuzzleStrategy<SupplyStacksModel>> strategies) : base(strategies)
        {
        }
    }
}
