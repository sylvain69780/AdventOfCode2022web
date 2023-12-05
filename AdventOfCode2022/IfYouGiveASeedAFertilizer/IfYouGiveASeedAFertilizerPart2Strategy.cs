using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IfYouGiveASeedAFertilizer
{
    public class IfYouGiveASeedAFertilizerPart2Strategy : IPuzzleStrategy<IfYouGiveASeedAFertilizerModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(IfYouGiveASeedAFertilizerModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var minValue = long.MaxValue;
            var seeds = model.Seeds!;
            var almanac = model.Almanac.ToDictionary(x => x.sourceCategory);
            var seedRanges = new List<(long start, long lenght)>();
            for (var i = 0; i < seeds.Length / 2; i++)
                seedRanges.Add((seeds[i*2], seeds[i*2 + 1]));
            var dfs = new Stack<(string category, long start, long lenght)>();
            foreach (var (start, lenght) in seedRanges)
                dfs.Push(("seed", start, lenght));
            while (dfs.TryPop(out var element))
            {
                if (almanac.TryGetValue(element.category, out var record))
                {
                    var ranges = record.ranges;
                    var newCategory = record.targetCategory;
                    var found = false;
                    foreach (var (destinationRangeStart, sourceRangeStart, rangeLenght) in ranges)
                    {
                        var start = Math.Max(sourceRangeStart, element.start);
                        var end = Math.Min(sourceRangeStart + rangeLenght, element.start + element.lenght);
                        var delta = destinationRangeStart - sourceRangeStart;
                        if ( end - start > 0 )
                        {
                            dfs.Push((newCategory, start + delta, end-start));
                            if (start - element.start > 0)
                                dfs.Push((element.category, element.start, start - element.start));
                            if (end - (element.start + element.lenght) > 0)
                                dfs.Push((element.category, end, end - (element.start + element.lenght)));
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        dfs.Push((newCategory, element.start, element.lenght));
                }
                else
                    minValue = Math.Min(minValue, element.start);
            }
            yield return updateContext();
            provideSolution(minValue.ToString());
        }
    }
}
