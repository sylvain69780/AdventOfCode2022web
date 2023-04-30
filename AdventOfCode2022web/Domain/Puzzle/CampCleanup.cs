using System.Text.RegularExpressions;

namespace AdventOfCode2022web.Domain.Puzzle
{
    public class CampCleanup : PuzzleSolver
    {
        private static string[] ToLines(string s) => s.Split("\n");
        private static string Format(int v) => v.ToString();

        private readonly struct Interval
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

            public bool Contains(Interval a) => a.Start >= Start && a.End <= End;

            public bool Overlaps(Interval a) => a.Start <= End && a.End >= Start;
        }

        private static Interval ToInterval(string a)
        {
            var b = a.Split("-");
            return new Interval(int.Parse(b[0]), int.Parse(b[1]));
        }

        private static IEnumerable<(Interval Interval1, Interval Interval2)> ListOfSectionAssignmentPairs(IEnumerable<string> records)
        {
            foreach (var a in records)
            {
                var b = a.Split(",");
                (string part1, string part2) = (b[0], b[1]);
                yield return (ToInterval(part1), ToInterval(part2));
            }
        }

        protected override string Part1(string puzzleInput)
        {
            var score = 0;
            foreach (var (a, b) in ListOfSectionAssignmentPairs(ToLines(puzzleInput)))
                if (a.Contains(b) || b.Contains(a)) 
                    score++;
            return Format(score);
        }

        protected override string Part2(string puzzleInput)
        {
            var score = 0;
            foreach (var (a, b) in ListOfSectionAssignmentPairs(ToLines(puzzleInput)))
                if (a.Overlaps(b))
                    score++;
            return Format(score);
        }
    }
}