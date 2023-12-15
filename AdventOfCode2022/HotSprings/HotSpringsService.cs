using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HotSprings
{
    public class HotSpringsService : SimplePuzzleService<HotSpringsModel>
    {
        public HotSpringsService(IEnumerable<IPuzzleStrategy<HotSpringsModel>> strategies) : base(strategies)
        {
        }

        public Dictionary<(int i, int groups), long>? Cache => _model.Cache;
    }
}
