using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PointOfIIcidence
{
    public class PointOfIIcidenceService : SimplePuzzleService<PointOfIIcidenceModel>
    {
        public PointOfIIcidenceService(IEnumerable<IPuzzleStrategy<PointOfIIcidenceModel>> strategies) : base(strategies)
        {
        }
    }
}
