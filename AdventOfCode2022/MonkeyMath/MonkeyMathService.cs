using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MonkeyMath
{
    public class MonkeyMathService : SimplePuzzleService<MonkeyMathModel>
    {
        public MonkeyMathService(IEnumerable<IPuzzleStrategy<MonkeyMathModel>> strategies) : base(strategies)
        {
        }
    }
}
