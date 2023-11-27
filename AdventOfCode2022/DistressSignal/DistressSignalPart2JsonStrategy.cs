﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.DistressSignal
{
    public partial class DistressSignalPart2JsonStrategy : IPuzzleStrategy<DistressSignalModel>
    {
        public string Name { get; set; } = "Part 2 using Json";

        public IEnumerable<ProcessingProgressModel> GetSteps(DistressSignalModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var packetStrings = model.PacketStrings!.Split("\n").Where(x => x != "")
                .Append("[[2]]").Append("[[6]]")
                .Select(x => (PacketString: x, Packet: PacketHelper.BuildPacket(x)))
                .OrderBy(x => x.Packet)
                .Select(x => x.PacketString).ToList();
            yield return updateContext();
            provideSolution(((1 + packetStrings.IndexOf("[[2]]")) * (1 + packetStrings.IndexOf("[[6]]"))).ToString());
        }
    }
}
