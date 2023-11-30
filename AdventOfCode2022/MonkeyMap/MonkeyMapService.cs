using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MonkeyMap
{
    public class MonkeyMapService : SimplePuzzleService<MonkeyMapModel>
    {
        public MonkeyMapService(IEnumerable<IPuzzleStrategy<MonkeyMapModel>> strategies) : base(strategies)
        {
        }
    }
}
