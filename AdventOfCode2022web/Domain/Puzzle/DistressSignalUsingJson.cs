using System.Dynamic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace AdventOfCode2022web.Domain.Puzzle
{
    [Puzzle(13, "Distress Signal")]
    public class DistressSignalUsingJson : IPuzzleSolver
    {
        public class JsonElementComparer : Comparer<JsonElement>
        {
            public override int Compare(JsonElement x, JsonElement y)
                => DistressSignalUsingJson.Compare(x, y);
        }

        public static int Compare(JsonElement x, JsonElement y)
        {
            if (x.ValueKind == JsonValueKind.Number && y.ValueKind == JsonValueKind.Number)
                return x.GetInt32().CompareTo(y.GetInt32());
            else if (x.ValueKind == JsonValueKind.Array && y.ValueKind == JsonValueKind.Array)
            {
                using var xEnumerator = x.EnumerateArray().GetEnumerator();
                using var yEnumerator = y.EnumerateArray().GetEnumerator();
                bool xMoveNext, yMoveNext;
                while (true)
                {
                    xMoveNext = xEnumerator.MoveNext(); // && xEnumerator.Current.ValueKind != JsonValueKind.Undefined;
                    yMoveNext = yEnumerator.MoveNext(); // && yEnumerator.Current.ValueKind != JsonValueKind.Undefined;
                    if (xMoveNext && !yMoveNext)
                        return 1;
                    else if (!xMoveNext && yMoveNext)
                        return -1;
                    else if (!xMoveNext && !yMoveNext)
                        return 0;
                    else
                    {
                        var tmp = Compare(xEnumerator.Current, yEnumerator.Current);
                        if (tmp != 0)
                            return tmp;
                    }
                }
            }
            else
            {
                if (x.ValueKind == JsonValueKind.Number)
                    return Compare(JsonSerializer.Deserialize<JsonElement>($"[{x.GetInt32()}]"), y);
                else
                    return Compare(x,JsonSerializer.Deserialize<JsonElement>($"[{y.GetInt32()}]"));

                //    int? firstIntegerOfJsonElement(JsonElement e)
                //    {
                //        if (e.ValueKind == JsonValueKind.Number)
                //            return e.GetInt32();
                //        else if (e.ValueKind == JsonValueKind.Array)
                //            using (var enumerator = e.EnumerateArray().GetEnumerator())
                //                if (enumerator.MoveNext() /* && enumerator.Current.ValueKind != JsonValueKind.Undefined */ )
                //                    return firstIntegerOfJsonElement(enumerator.Current);
                //        return null;
                //    }
                //    var xInteger = firstIntegerOfJsonElement(x);
                //    var yInteger = firstIntegerOfJsonElement(y);
                //    if (xInteger == null && yInteger == null)
                //        return 0;
                //    else if (yInteger == null)
                //        return 1;
                //    else if (xInteger == null)
                //        return -1;
                //    else if (xInteger.Value == yInteger.Value)
                //        return x.ValueKind == JsonValueKind.Array ? 1 : -1;
                //    else
                //        return xInteger.Value.CompareTo(yInteger.Value);
                //
                }
            }

            public IEnumerable<string> SolveFirstPart(string puzzleInput)
        {
            var packetStrings = @"[" + puzzleInput.Replace("\n\n", "\n").Replace("\n", ",") + "]";
            var packets = JsonSerializer.Deserialize<JsonElement[]>(packetStrings);
            var wellOrderedPackets = 0;
            for (var pairId = 0; pairId < packets!.Length / 2; pairId++)
            {
                if (Compare(packets[pairId * 2], packets[pairId * 2 + 1]) < 0 ) 
                    wellOrderedPackets += pairId+1;
            }
            yield return wellOrderedPackets.ToString();
        }
        public IEnumerable<string> SolveSecondPart(string puzzleInput)
        {
            var packetStrings = @"[[[2]],[[6]]," + puzzleInput.Replace("\n\n", "\n").Replace("\n", ",") + "]";
            var packets = JsonSerializer.Deserialize<JsonElement[]>(packetStrings);
            Array.Sort(packets!, new JsonElementComparer());
            int firstPacket = 0,secondPacket = 0;
            StringBuilder a = new();
            for (var index = 0; index < packets!.Length; index++)
            {
                var serializedPacket = JsonSerializer.Serialize(packets[index]);
                a.Append(serializedPacket + "\n");
                if (serializedPacket == "[[2]]")
                    firstPacket = index+1;
                else if (serializedPacket == "[[6]]")
                    secondPacket = index + 1;
            }
            // yield return a.ToString();
            yield return (firstPacket * secondPacket).ToString();
        }
    }
}
