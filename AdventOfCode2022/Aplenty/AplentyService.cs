using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aplenty
{
    public class AplentyService : SimplePuzzleService<AplentyModel>
    {
        public AplentyService(IEnumerable<IPuzzleStrategy<AplentyModel>> strategies) : base(strategies)
        {
        }
    }
}
