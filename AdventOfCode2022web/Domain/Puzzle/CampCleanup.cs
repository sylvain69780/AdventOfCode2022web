namespace AdventOfCode2022web.Domain.Puzzle
{
    public class CampCleanup : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            var r = (string x) => { var r = x.Split("-"); return (start: int.Parse(r[0]), end: int.Parse(r[1])); };
            var score = 0;
            foreach (var (a, b) in Input.Split("\n").Select(x => x.Split(",")).Select(x => (r(x[0]), r(x[1]))))
            {
                if ((b.start >= a.start && b.end <= a.end) || (a.start >= b.start && a.end <= b.end))
                    score++;
            }
            return score.ToString();
        }
        public string Part2()
        {
            var r = (string x) => { var r = x.Split("-"); return (start: int.Parse(r[0]), end: int.Parse(r[1])); };
            var score = 0;
            foreach (var (a, b) in Input.Split("\n").Select(x => x.Split(",")).Select(x => (r(x[0]), r(x[1]))))
            {
                if ((a.start <= b.start && b.start <= a.end) || (b.start <= a.start && a.start <= b.end))
                    score++;
            }
            return score.ToString();
        }
    }
}