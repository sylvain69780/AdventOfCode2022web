using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Trebuchet
{
    public class TrebuchetService : SimplePuzzleService<TrebuchetModel>
    {
        public TrebuchetService(IEnumerable<IPuzzleStrategy<TrebuchetModel>> strategies) : base(strategies)
        {
        }
    }
}
