namespace Domain.CampCleanup
{
    public class CampCleanupSolution : IPuzzleSolution
    {

        private string[] _lines = Array.Empty<string>();

        public void Initialize(string s)
        {
            _lines = s.Split("\n");
        }

        private static string Format(int v) => v.ToString();

        private static Interval ToInterval(string intervalStr)
        {
            var split = intervalStr.Split("-");
            return new Interval(int.Parse(split[0]), int.Parse(split[1]));
        }

        private static IEnumerable<(Interval Interval1, Interval Interval2)> ListOfSectionAssignmentPairs(IEnumerable<string> records)
        {
            foreach (var record in records)
            {
                var split = record.Split(",");
                (string intervalStr1, string intervalStr2) = (split[0], split[1]);
                yield return (ToInterval(intervalStr1), ToInterval(intervalStr2));
            }
        }

        public IEnumerable<string> SolveFirstPart()
        {
            var score = 0;
            foreach (var (interval1, interval2) in ListOfSectionAssignmentPairs(_lines))
                if (interval1.Contains(interval2) || interval2.Contains(interval1))
                    score++;
            yield return Format(score);
        }

        public IEnumerable<string> SolveSecondPart()
        {
            var score = 0;
            foreach (var (interval1, interval2) in ListOfSectionAssignmentPairs(_lines))
                if (interval1.Overlaps(interval2))
                    score++;
            yield return Format(score);
        }
    }
}