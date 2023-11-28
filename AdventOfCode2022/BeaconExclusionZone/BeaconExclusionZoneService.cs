using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BeaconExclusionZone
{
    public class BeaconExclusionZoneService : SimplePuzzleService<BeaconExclusionZoneModel>
    {
        public BeaconExclusionZoneService(IEnumerable<IPuzzleStrategy<BeaconExclusionZoneModel>> strategies) : base(strategies)
        {
        }
    }
}
