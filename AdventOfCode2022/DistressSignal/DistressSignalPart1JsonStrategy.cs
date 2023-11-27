using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.DistressSignal
{
    public partial class DistressSignalPart1JsonStrategy : IPuzzleStrategy<DistressSignalModel>
    {
        public string Name { get; set; } = "Part 1 using Json";

        public IEnumerable<ProcessingProgressModel> GetSteps(DistressSignalModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var packetStrings = @"[" + model.PacketStrings!.Replace("\n\n", "\n").Replace("\n", ",") + "]";
            var packets = JsonSerializer.Deserialize<JsonElement[]>(packetStrings);
            var wellOrderedPackets = 0;
            for (var pairId = 0; pairId < packets!.Length / 2; pairId++)
            {
                if (JsonHelpers.Compare(packets[pairId * 2], packets[pairId * 2 + 1]) < 0)
                    wellOrderedPackets += pairId + 1;
            }
            yield return updateContext();
            provideSolution(wellOrderedPackets.ToString());
        }
    }
}
