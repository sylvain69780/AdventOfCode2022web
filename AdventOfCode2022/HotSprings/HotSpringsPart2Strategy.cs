using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Domain.HotSprings
{
    public class HotSpringsPart2Strategy : IPuzzleStrategy<HotSpringsModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(HotSpringsModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var rows = model.Rows!;
            rows = rows.Select(row => (springs: row.springs + '?' + row.springs + '?' + row.springs + '?' + row.springs + '?' + row.springs,
            ranges: row.ranges.Concat(row.ranges).Concat(row.ranges).Concat(row.ranges).Concat(row.ranges).ToArray())
            ).ToArray();
            var sum = 0L;
            foreach (var (springs, ranges) in rows)
            {
                var cache = new Dictionary<(int i, int groups), long>();
                var counter = Solve(springs, ranges, ranges.Length, 0, cache);
                sum += counter;
            }
            yield return updateContext();
            provideSolution(sum.ToString());
        }

        long Solve(string springs, long[] ranges, int groups, int i, Dictionary<(int i, int groups), long> cache)
        {
            // https://www.reddit.com/r/adventofcode/comments/18hg99r/2023_day_12_simple_tutorial_with_memoization/
            // ranges = groups
            if (groups == 0)
            {
                var found = false;
                for (var a = i; a < springs.Length; a++)
                {
                    var c = springs[a];
                    if (c == '#')
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    return 1;
                else
                    return 0;
            }
            while (i < springs.Length && springs[i] == '.')
                i++;
            if (i >= springs.Length)
                return 0;

            if (cache.TryGetValue((i, groups), out var value))
                return value;

            var ret = 0L;
            var l = (int)ranges[^groups];
            // try fill with current group
            if (i <= springs.Length - l)
            {
                var found = false;
                var pat = new string('#', l)+'.';
                for (var a = 0; a<pat.Length && i+a<springs.Length; a++)
                {
                    var c = springs[i+a];
                    if (c != pat[a] && c != '?')
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                        ret += Solve(springs, ranges, groups - 1, i + l + 1, cache);
            }
            // advance of 1 
            if (springs[i] == '?')
                ret += Solve(springs, ranges, groups, i + 1, cache);
            cache.Add((i, groups), ret);
            return ret;
        }

        public IEnumerable<ProcessingProgressModel> GetStepsResign20231213(HotSpringsModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            //          var test = "?###????????".PossibleConfigurationsOptim(new long[] { 3, 2, 1 }).ToArray();
            //            var rows = model.Rows!.Select(x => (springs:x.springs + '?',x.ranges )).ToArray();
            var rows = model.Rows!;
            var sum = 0L;
            foreach (var (springs, ranges) in rows)
            {
                //var sol = (long)springs.PossibleConfigurationsOptim(ranges);
                //var sol2 = (long)(springs + "?" + springs).PossibleConfigurationsOptim(ranges.Concat(ranges).ToArray());
                //var sol3 = (long)(springs + "?" + springs + "?" + springs).PossibleConfigurationsOptim(ranges.Concat(ranges).Concat(ranges).ToArray());
                //var diff = sol2 / sol;
                //var remains = sol2 % sol;

                var set1 = springs.PossibleConfigurationsOptimBAD(ranges).ToArray();
                var set11 = set1.SelectMany(x => set1.Select(y => x + '.' + y)).ToHashSet();
                var set2 = (springs + "?" + springs).PossibleConfigurationsOptimBAD(ranges).ToArray();
                var set3 = set2.Where(x => !set11.Contains(x));
                var count = 0L;
                var diff = set3.Count();
                count = set1.Length * (set1.Length + diff) * (set1.Length + diff) * (set1.Length + diff) * (set1.Length + diff);
                // DEBUG in watch (springs+'?'+springs).PossibleConfigurationsOptimBAD(ranges.Concat(ranges).ToArray()).ToArray()
                //if (remains != 0)
                //{
                //    // ;-(
                //    var sol3 = (long)(springs + "?" + springs + "?" + springs + "?" + springs + "?" + springs).PossibleConfigurationsOptim(ranges.Concat(ranges).Concat(ranges).Concat(ranges).Concat(ranges).ToArray());
                //    count = sol3;
                //}
                //else
                //    count = sol * diff * diff * diff * diff;
                yield return updateContext();
                sum += count;
            }
            yield return updateContext();
            provideSolution(sum.ToString());
        }
    }
}

//var sum = rows.Select(row => row.springs.PossibleConfigurations()
//    .Select(x => (springs: x, ranges: x.Groups().ToArray()))
//    .Where(x => x.ranges.Length == row.ranges.Length && !Enumerable.Range(0, x.ranges.Length).Any(i => x.ranges[i] != row.ranges[i]))
//    .Select(x => (
//        springs: x.springs + '?' + x.springs + '?' + x.springs + '?' + x.springs + '?' + x.springs,
//        ranges: x.ranges.Concat(x.ranges).Concat(x.ranges).Concat(x.ranges).Concat(x.ranges).ToArray())
//        )
//    .SelectMany(x => x.springs.PossibleConfigurations()
//        .Select(y => (springs: y, ranges: y.Groups().ToArray()))
//        .Where(y => x.ranges.Length == y.ranges.Length && !Enumerable.Range(0, x.ranges.Length).Any(i => x.ranges[i] != y.ranges[i]))
//        )
//    .Count())
//    //                .Select(x => x*x*x*x*x)
//    .Sum();
