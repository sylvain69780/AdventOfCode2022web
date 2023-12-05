using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IfYouGiveASeedAFertilizer
{
    public readonly struct Interval
    {
        public long Start { get; }
        public long End { get; }

        public Interval(long start, long end)
        {
            if (end < start)
                throw new ArgumentException("End must be greater than or equal to start.");
            Start = start;
            End = end;
        }

        public bool Contains(Interval interval) => interval.Start >= Start && interval.End <= End;

        public bool Overlaps(Interval interval) => interval.Start <= End && interval.End >= Start;
    }
}
