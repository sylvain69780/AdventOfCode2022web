namespace Domain.CampCleanup
{
    public class CampCleanupModel : IPuzzleModel
    {
        public List<(Interval Interval1, Interval Interval2)>? ListOfSectionAssignmentPairs { get; set; } = new();

        private static Interval ToInterval(string intervalStr)
        {
            var split = intervalStr.Split("-");
            return new Interval(int.Parse(split[0]), int.Parse(split[1]));
        }
        public void Parse(string input)
        {
            foreach (var record in input.Split('\n'))
            {
                var split = record.Split(",");
                (string intervalStr1, string intervalStr2) = (split[0], split[1]);
                ListOfSectionAssignmentPairs!.Add((ToInterval(intervalStr1), ToInterval(intervalStr2)));
            }
        }
    }
}
