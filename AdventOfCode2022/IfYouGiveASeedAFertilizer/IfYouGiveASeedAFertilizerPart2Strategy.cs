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
                        //var current = new Interval(element.start, element.start + element.lenght);
                        //var source = new Interval(sourceRangeStart, sourceRangeStart + rangeLenght);
                        var delta = destinationRangeStart - sourceRangeStart;

                        var start = Math.Max(sourceRangeStart, element.start);
                        var end = Math.Min(sourceRangeStart + rangeLenght, element.start + element.lenght);
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

                        //if (sourceRangeStart >= element.start && sourceRangeStart <= element.start + element.lenght)
                        //{
                        //    // cut beggin
                        //    if (sourceRangeStart - element.start > 0)
                        //        dfs.Push((element.category, element.start, sourceRangeStart - element.start));
                        //    // move other part
                        //    var l = Math.Min(sourceRangeStart + rangeLenght, element.start + element.lenght) - sourceRangeStart;
                        //    if (l > 0)
                        //        dfs.Push((newCategory, sourceRangeStart, l));
                        //}
                        //if ( current.Contains(source))
                        //{
                        //    dfs.Push((newCategory, current.Start+delta,source.Start-current.Start));
                        //    dfs.Push((newCategory, source.End+delta,current.End-source.End));
                        //}
                        //else if ( current.Overlaps(source))
                        //{
                        //    dfs.Push((newCategory,delta+ Math.Max(source.Start,current.Start),Math.Min(source.End,current.End)- Math.Max(source.Start, current.Start)));
                        //}
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
