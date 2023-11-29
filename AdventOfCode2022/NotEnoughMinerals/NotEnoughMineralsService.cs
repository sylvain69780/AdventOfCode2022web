using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NotEnoughMinerals
{
    public class NotEnoughMineralsService : SimplePuzzleService<NotEnoughMineralsModel>
    {
        public NotEnoughMineralsService(IEnumerable<IPuzzleStrategy<NotEnoughMineralsModel>> strategies) : base(strategies)
        {
        }
    }
}
