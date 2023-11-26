using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NoSpaceLeftOnDevice
{
    public class NoSpaceLeftOnDeviceService : SimplePuzzleService<NoSpaceLeftOnDeviceModel>
    {
        public NoSpaceLeftOnDeviceService(IEnumerable<IPuzzleStrategy<NoSpaceLeftOnDeviceModel>> strategies) : base(strategies)
        {
        }
    }
}
