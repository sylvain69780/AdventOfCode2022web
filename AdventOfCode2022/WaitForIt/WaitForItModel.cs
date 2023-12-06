using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.WaitForIt
{
    public class WaitForItModel : IPuzzleModel
    {
        List<(long time, long distance)> _races = new();
        public List<(long time, long distance)> Races => _races;
        public void Parse(string input)
        {
            _races.Clear();
            var records = input.Replace("\r", "").Split("\n");
            var timesMs = records[0].Replace("Time:      ", "").Split(" ").Where(x => x != string.Empty).Select(x => long.Parse(x)).ToList();
            var distancesMm = records[1].Replace("Distance:  ", "").Split(" ").Where(x => x != string.Empty).Select(x => long.Parse(x)).ToList();
            for ( var i= 0; i < timesMs.Count; i++)
            {
                _races.Add((timesMs[i], distancesMm[i]));
            }
        }

        public static long FindRange(long time, long distance)
        {
            var lower = 1L;
            var upper = time - 1;
            while (upper - lower > 0)
            {
                var m = (lower + upper) / 2;
                if (lower * (time - lower) > upper * (time - upper))
                    upper = upper - lower == 1 ? lower : m;
                else
                    lower = upper - lower == 1 ? upper : m;
            }
            var optimal = upper;
            lower = 1L;
            upper = optimal;
            while (upper - lower > 0)
            {
                var m = (lower + upper) / 2;
                if (m * (time - m) - distance > 0)
                    upper = upper - lower == 1 ? lower : m;
                else
                    lower = upper - lower == 1 ? upper : m;
            }
            var start = upper;
            lower = optimal;
            upper = distance - 1;
            while (upper - lower > 0)
            {
                var m = (lower + upper) / 2;
                if ((decimal)m * (decimal)(time - m) - distance > 0) // overflow with long ! switch to decimal
                    lower = upper - lower == 1 ? upper : m;
                else
                    upper = upper - lower == 1 ? lower : m;
            }
            var end = upper;
            var range = end - start;
            return range;
        }

    }
}
