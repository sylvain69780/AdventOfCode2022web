using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TuningTrouble
{
    public class TuningTroubleService : SimplePuzzleService<TuningTroubleModel>
    {
        public TuningTroubleService(IEnumerable<IPuzzleStrategy<TuningTroubleModel>> strategies) : base(strategies)
        {
        }
    }
}
