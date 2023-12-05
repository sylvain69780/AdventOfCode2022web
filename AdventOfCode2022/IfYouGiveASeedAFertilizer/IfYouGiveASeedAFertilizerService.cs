using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IfYouGiveASeedAFertilizer
{
    public class IfYouGiveASeedAFertilizerService : SimplePuzzleService<IfYouGiveASeedAFertilizerModel>
    {
        public IfYouGiveASeedAFertilizerService(IEnumerable<IPuzzleStrategy<IfYouGiveASeedAFertilizerModel>> strategies) : base(strategies)
        {
        }
    }
}
