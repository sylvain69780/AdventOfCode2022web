using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DistressSignal
{
    public class DistressSignalService : SimplePuzzleService<DistressSignalModel>
    {
        public DistressSignalService(IEnumerable<IPuzzleStrategy<DistressSignalModel>> strategies) : base(strategies)
        {
        }
    }
}
