using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IfYouGiveASeedAFertilizer
{
    public class IfYouGiveASeedAFertilizerModel : IPuzzleModel
    {
        List<(string sourceCategory, string targetCategory, List<(long destinationRangeStart, long sourceRangeStart, long rangeLenght)>)> _almanac = new();
        public List<(string sourceCategory, string targetCategory, List<(long destinationRangeStart, long sourceRangeStart, long rangeLenght)> ranges)> Almanac => _almanac;

        long[]? _seeds;
        public long[]? Seeds => _seeds;

        public void Parse(string input)
        {
            var records = input.Replace("\r", "").Split("\n\n");
            _seeds = records[0].Replace("seeds: ", "").Split(' ').Select(x => long.Parse(x)).ToArray();
            var almanac = _almanac;
            almanac.Clear();
            foreach (var record in records[1..])
            {
                var lines = record.Split("\n");
                var mapName = lines[0].Replace(" map:", "").Split("-to-");
                var ranges = (sourceCategory: mapName[0], targetCategory: mapName[1], ranges : new List<(long destinationRangeStart, long sourceRangeStart, long rangeLenght)>());
                foreach (var line in lines[1..])
                {
                    var rec = line.Split(' ').Select(x => long.Parse(x)).ToArray();
                    var r = (destinationRangeStart: rec[0], sourceRangeStart: rec[1], rangeLenght: rec[2]);
                    ranges.ranges.Add(r);
                }
                almanac.Add(ranges);
            }
        }
    }
}
