using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RucksackReorganization
{
    public class RucksackReorganizationService : SimplePuzzleService<RucksackReorganizationModel>
    {
        public RucksackReorganizationService(IEnumerable<IPuzzleStrategy<RucksackReorganizationModel>> strategies) : base(strategies)
        {
        }
    }
}
