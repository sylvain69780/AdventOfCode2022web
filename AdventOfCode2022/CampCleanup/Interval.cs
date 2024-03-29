﻿namespace Domain.CampCleanup
{
    public readonly struct Interval
    {
        public int Start { get; }
        public int End { get; }

        public Interval(int start, int end)
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
