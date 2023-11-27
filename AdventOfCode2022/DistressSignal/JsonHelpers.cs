using System.Text.Json;

namespace Domain.DistressSignal
{
    internal static class JsonHelpers
    {

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
    }
}