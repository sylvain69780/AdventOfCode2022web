using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.WaitForIt
{
    public class WaitForItService : SimplePuzzleService<WaitForItModel>
    {
        public WaitForItService(IEnumerable<IPuzzleStrategy<WaitForItModel>> strategies) : base(strategies)
        {
        }
    }
}
