namespace Domain.DistressSignal
{
    public class PacketHelper
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
}

