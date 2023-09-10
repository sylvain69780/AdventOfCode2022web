using System.Linq;
using System.Text;
using System.Text.Json;
using AdventOfCode2022Solutions.PuzzleSolutions;

namespace AdventOfCode2022Solutions.PuzzleSolutions.DistressSignal
{
    [Puzzle(13, "Distress Signal")]
    public class DistressSignalUsingJsonSolution : IPuzzleSolution
    {
        private string _puzzleInput = string.Empty;
        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }

        public class JsonElementComparer : Comparer<JsonElement>
        {
            public override int Compare(JsonElement x, JsonElement y)
                => DistressSignalUsingJsonSolution.Compare(x, y);
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
                    xMoveNext = xEnumerator.MoveNext();
                    yMoveNext = yEnumerator.MoveNext();
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
                    return Compare(x, JsonSerializer.Deserialize<JsonElement>($"[{y.GetInt32()}]"));
            }
        }

        public string SolveFirstPart()
        {
            var packetStrings = @"[" + _puzzleInput.Replace("\n\n", "\n").Replace("\n", ",") + "]";
            var packets = JsonSerializer.Deserialize<JsonElement[]>(packetStrings);
            var wellOrderedPackets = 0;
            for (var pairId = 0; pairId < packets!.Length / 2; pairId++)
            {
                if (Compare(packets[pairId * 2], packets[pairId * 2 + 1]) < 0)
                    wellOrderedPackets += pairId + 1;
            }
            return wellOrderedPackets.ToString();
        }
        public string SolveSecondPart()
        {
            var packetStrings = @"[[[2]],[[6]]," + _puzzleInput.Replace("\n\n", "\n").Replace("\n", ",") + "]";
            var packets = JsonSerializer.Deserialize<JsonElement[]>(packetStrings);
            Array.Sort(packets!, new JsonElementComparer());
            int firstPacket = 0, secondPacket = 0;
            StringBuilder a = new();
            for (var index = 0; index < packets!.Length; index++)
            {
                var serializedPacket = JsonSerializer.Serialize(packets[index]);
                a.Append(serializedPacket + "\n");
                if (serializedPacket == "[[2]]")
                    firstPacket = index + 1;
                else if (serializedPacket == "[[6]]")
                    secondPacket = index + 1;
            }
            return (firstPacket * secondPacket).ToString(); // + "\n" + string.Join('\n',packets);
        }
    }
}
