namespace AdventOfCode2022web.Domain.Puzzle
{
    public class CampCleanup : PuzzleSolver
    {
        protected override string Part1(string inp)
        {
            var r = (string x) => { var r = x.Split("-"); return (start: int.Parse(r[0]), end: int.Parse(r[1])); };
            var score = 0;
            foreach (var (a, b) in inp.Split("\n").Select(x => x.Split(",")).Select(x => (r(x[0]), r(x[1]))))
            {
                if ((b.start >= a.start && b.end <= a.end) || (a.start >= b.start && a.end <= b.end))
                    score++;
            }
            return score.ToString();
        }
        protected override string Part2(string inp)
        {
            var r = (string x) => { var r = x.Split("-"); return (start: int.Parse(r[0]), end: int.Parse(r[1])); };
            var score = 0;
            foreach (var (a, b) in inp.Split("\n").Select(x => x.Split(",")).Select(x => (r(x[0]), r(x[1]))))
            {
                if ((a.start <= b.start && b.start <= a.end) || (b.start <= a.start && a.start <= b.end))
                    score++;
            }
            return score.ToString();
        }
    }
}