using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TheFloorWillBeLava
{
    public class TheFloorWillBeLavaService : SimplePuzzleService<TheFloorWillBeLavaModel>
    {
        public TheFloorWillBeLavaService(IEnumerable<IPuzzleStrategy<TheFloorWillBeLavaModel>> strategies) : base(strategies)
        {
        }
    }
}
