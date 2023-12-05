using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Scratchcards
{
    public class ScratchcardsService : SimplePuzzleService<ScratchcardsModel>
    {
        public ScratchcardsService(IEnumerable<IPuzzleStrategy<ScratchcardsModel>> strategies) : base(strategies)
        {
        }
    }
}
