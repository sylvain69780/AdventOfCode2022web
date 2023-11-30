using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.GrovePositioningSystem
{
    public class GrovePositioningSystemService : SimplePuzzleService<GrovePositioningSystemModel>
    {
        public GrovePositioningSystemService(IEnumerable<IPuzzleStrategy<GrovePositioningSystemModel>> strategies) : base(strategies)
        {
        }
    }
}
