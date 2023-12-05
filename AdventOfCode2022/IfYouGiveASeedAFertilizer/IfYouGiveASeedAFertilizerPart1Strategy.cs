using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IfYouGiveASeedAFertilizer
{
    public class IfYouGiveASeedAFertilizerPart1Strategy : IPuzzleStrategy<IfYouGiveASeedAFertilizerModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(IfYouGiveASeedAFertilizerModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var almanac = model.Almanac.ToDictionary(x => x.sourceCategory);
            var minValue = long.MaxValue;
            foreach (var seed in model.Seeds!)
            {
                var start = "seed";
                var id = seed;
                while (almanac.TryGetValue(start, out var record))
                {
                    start = record.targetCategory;
                    foreach (var (destinationRangeStart, sourceRangeStart, rangeLenght) in record.ranges)
                    {
                        if ( id >= sourceRangeStart && id <= sourceRangeStart+rangeLenght)
                        {
                            id = destinationRangeStart + id - sourceRangeStart;
                            break;
                        }
                    }
                }
                minValue = Math.Min(id, minValue);
            }
            yield return updateContext();
            provideSolution(minValue.ToString());
        }
    }
}
