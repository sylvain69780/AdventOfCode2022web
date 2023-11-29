using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PyroclasticFlow
{
    public class PyroclasticFlowService : SimplePuzzleService<PyroclasticFlowModel>
    {
        public PyroclasticFlowService(IEnumerable<IPuzzleStrategy<PyroclasticFlowModel>> strategies) : base(strategies)
        {
        }
    }
}
