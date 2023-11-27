using System.Text.Json;

namespace Domain.DistressSignal
{
    public partial class DistressSignalPart1JsonStrategy
    {
        public class JsonElementComparer : Comparer<JsonElement>
        {
            public override int Compare(JsonElement x, JsonElement y)
                => DistressSignalUsingJsonSolution.Compare(x, y);
        }

    }
}
