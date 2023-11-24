using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RockPaperScissors
{
    public class RockPaperScissorsService : SimplePuzzleService<RockPaperScissorsModel>
    {
        public RockPaperScissorsService(IEnumerable<IPuzzleStrategy<RockPaperScissorsModel>> strategies) : base(strategies)
        {
        }
    }
}
