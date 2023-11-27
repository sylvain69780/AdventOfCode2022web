using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DistressSignal
{
    public class DistressSignalModel : IPuzzleModel
    {
        string? _packetStrings;
        public string? PacketStrings => _packetStrings;
        public void Parse(string input)
        {
            _packetStrings = input;
        }
    }
}
