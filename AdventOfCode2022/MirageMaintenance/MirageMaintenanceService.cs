using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MirageMaintenance
{
    public class MirageMaintenanceService : SimplePuzzleService<MirageMaintenanceModel>
    {
        public MirageMaintenanceService(IEnumerable<IPuzzleStrategy<MirageMaintenanceModel>> strategies) : base(strategies)
        {
        }
    }
}
