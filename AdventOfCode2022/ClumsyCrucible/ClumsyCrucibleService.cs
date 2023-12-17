using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ClumsyCrucible
{
    public class ClumsyCrucibleService : SimplePuzzleService<ClumsyCrucibleModel>
    {
        public ClumsyCrucibleService(IEnumerable<IPuzzleStrategy<ClumsyCrucibleModel>> strategies) : base(strategies)
        {
        }
    }
}
