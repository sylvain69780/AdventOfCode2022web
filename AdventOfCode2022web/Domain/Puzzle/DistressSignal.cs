namespace AdventOfCode2022web.Domain.Puzzle
{
    [Puzzle(13, "Distress Signal")]
    public class DistressSignal : IPuzzleSolver
    {
        private static string[] ToLines(string s) => s.Split("\n");

        class IntegerPacket : Packet
        {
            public int Integer;
        }
        class ListPacket : Packet
        {
            public List<Packet>? List;
        }
        class Packet : IComparable<Packet>
        {
            int IComparable<Packet>.CompareTo(Packet? right)
            {
                var left = this;
                var leftInteger = left as IntegerPacket;
                var rightInteger = right as IntegerPacket;
                if (leftInteger != null && rightInteger != null)
                    return leftInteger.Integer.CompareTo(rightInteger.Integer);
                var leftPacket = leftInteger == null ? (ListPacket)left : new ListPacket { List = new List<Packet> { left } };
                var rightPacket = rightInteger == null ? (ListPacket)right! : new ListPacket { List = new List<Packet> { right! } };
                for (var i = 0; i < Math.Min(leftPacket.List!.Count, rightPacket.List!.Count); i++)
                {
                    int res = ((IComparable<Packet>)leftPacket.List[i]).CompareTo(rightPacket.List[i]);
                    if (res != 0) return res;
                }
                return leftPacket.List.Count.CompareTo(rightPacket.List.Count);
            }
        }
        class PacketHelper
        {
            public static Packet BuildPacket(string packetString)
            {
                if (packetString[0] == '[')
                {
                    if (packetString[1] == ']')
                        return new ListPacket { List = new List<Packet>() };
                    var childPackets = new List<Packet>();
                    var level = 0;
                    var beginString = 1;
                    for (var endString = 1; endString < packetString.Length - 2; endString++)
                    {
                        if (packetString[endString] == '[') 
                            level++;
                        else if (packetString[endString] == ']') 
                            level--;
                        else if (packetString[endString] == ',' && level == 0)
                        {
                            childPackets.Add(BuildPacket(packetString[beginString..endString]));
                            beginString = endString + 1;
                        }
                    }
                    childPackets.Add(BuildPacket(packetString[beginString..(packetString.Length - 1)]));
                    return new ListPacket { List = childPackets };
                }
                else
                    return new IntegerPacket() { Integer = int.Parse(packetString) };
            }
        }

        public IEnumerable<string> SolveFirstPart(string puzzleInput)
        {
            var packetStrings = ToLines(puzzleInput);
            var wellOrderedPackets = 0;
            for (var pairId = 1; pairId * 3 <= packetStrings.Length + 1; pairId++)
            {
                var firstPacket = PacketHelper.BuildPacket(packetStrings[pairId * 3 - 3]);
                var secondPacket = PacketHelper.BuildPacket(packetStrings[pairId * 3 - 2]);
                if ( ((IComparable<Packet>)secondPacket).CompareTo(firstPacket) > 0 ) 
                    wellOrderedPackets += pairId;
            }
            yield return wellOrderedPackets.ToString();
        }
        public IEnumerable<string> SolveSecondPart(string puzzleInput)
        {
            var packetStrings = ToLines(puzzleInput).Where(x => x != "")
                .Append("[[2]]").Append("[[6]]")
                .Select(x => (PacketString: x, Packet: PacketHelper.BuildPacket(x)))
                .OrderBy(x => x.Packet)
                .Select(x => x.PacketString).ToList();
            yield return ((1 + packetStrings.IndexOf("[[2]]")) * (1 + packetStrings.IndexOf("[[6]]"))).ToString();
        }
    }
}