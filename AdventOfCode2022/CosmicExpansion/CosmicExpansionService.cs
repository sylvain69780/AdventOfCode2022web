using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CosmicExpansion
{
    public class CosmicExpansionService : SimplePuzzleService<CosmicExpansionModel>
    {
        public CosmicExpansionService(IEnumerable<IPuzzleStrategy<CosmicExpansionModel>> strategies) : base(strategies)
        {
        }
    }
}
