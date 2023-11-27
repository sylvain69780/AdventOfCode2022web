using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MonkeyInTheMiddle
{
    public class MonkeyInTheMiddleService : SimplePuzzleService<MonkeyInTheMiddleModel>
    {
        public MonkeyInTheMiddleService(IEnumerable<IPuzzleStrategy<MonkeyInTheMiddleModel>> strategies) : base(strategies)
        {
        }
    }
}
