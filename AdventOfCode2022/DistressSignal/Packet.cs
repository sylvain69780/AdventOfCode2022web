namespace Domain.DistressSignal
{
    public class Packet : IComparable<Packet>
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
}
