namespace Domain.DistressSignal
{
    public partial class DistressSignalSolution : IPuzzleSolution
    {
        private string _puzzleInput = string.Empty;
        public void Initialize(string puzzleInput) => _puzzleInput = puzzleInput;

        private static string[] ToLines(string s) => s.Split("\n");

        public IEnumerable<string> SolveFirstPart()
        {
            var packetStrings = ToLines(_puzzleInput);
            var wellOrderedPackets = 0;
            for (var pairId = 1; pairId * 3 <= packetStrings.Length + 1; pairId++)
            {
                var firstPacket = PacketHelper.BuildPacket(packetStrings[pairId * 3 - 3]);
                var secondPacket = PacketHelper.BuildPacket(packetStrings[pairId * 3 - 2]);
                if (((IComparable<Packet>)secondPacket).CompareTo(firstPacket) > 0)
                    wellOrderedPackets += pairId;
            }
            yield return wellOrderedPackets.ToString();
        }
        public IEnumerable<string> SolveSecondPart()
        {
            var packetStrings = ToLines(_puzzleInput).Where(x => x != "")
                .Append("[[2]]").Append("[[6]]")
                .Select(x => (PacketString: x, Packet: PacketHelper.BuildPacket(x)))
                .OrderBy(x => x.Packet)
                .Select(x => x.PacketString).ToList();
            yield return ((1 + packetStrings.IndexOf("[[2]]")) * (1 + packetStrings.IndexOf("[[6]]"))).ToString();
        }
    }
}

