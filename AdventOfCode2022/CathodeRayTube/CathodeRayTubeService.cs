using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CathodeRayTube
{
    public class CathodeRayTubeService : SimplePuzzleService<CathodeRayTubeModel>
    {
        public CathodeRayTubeService(IEnumerable<IPuzzleStrategy<CathodeRayTubeModel>> strategies) : base(strategies)
        {
        }
    }
}
