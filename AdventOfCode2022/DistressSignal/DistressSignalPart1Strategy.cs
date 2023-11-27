using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DistressSignal
{
    public class DistressSignalPart1Strategy : IPuzzleStrategy<DistressSignalModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(DistressSignalModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var packetStrings = model.PacketStrings!.Split("\n");
            var wellOrderedPackets = 0;
            for (var pairId = 1; pairId * 3 <= packetStrings.Length + 1; pairId++)
            {
                var firstPacket = PacketHelper.BuildPacket(packetStrings[pairId * 3 - 3]);
                var secondPacket = PacketHelper.BuildPacket(packetStrings[pairId * 3 - 2]);
                if (((IComparable<Packet>)secondPacket).CompareTo(firstPacket) > 0)
                    wellOrderedPackets += pairId;
            }
            yield return updateContext();
            provideSolution(wellOrderedPackets.ToString());
        }
    }
}
