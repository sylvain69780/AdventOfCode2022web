using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CamelCards
{
    public class CamelCardsService : SimplePuzzleService<CamelCardsModel>
    {
        public CamelCardsService(IEnumerable<IPuzzleStrategy<CamelCardsModel>> strategies) : base(strategies)
        {
        }
    }
}
