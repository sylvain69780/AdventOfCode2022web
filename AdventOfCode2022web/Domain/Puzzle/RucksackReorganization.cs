namespace AdventOfCode2022web.Domain.Puzzle
{
    public class RucksackReorganization : PuzzleSolver
    {
        protected override string Part1(string inp)
        {
            var score = 0;
            foreach (var rucksack in inp.Split("\n"))
            {
                var l = rucksack.Length;
                var (a, b) = (rucksack.Substring(0, l / 2), rucksack.Substring(l / 2, l / 2));
                var r = (int)a.First(x => b.Contains(x));
                var p = r >= (int)'a' ? r - (int)'a' + 1 : r - (int)'A' + 27;
                score += p;
            }
            return score.ToString();
        }
        protected override string Part2(string inp)
        {
            var score = 0;
            var rucksacks = inp.Split("\n");
            for (var i = 0; i < rucksacks.Length / 3; i++)
            {
                var (a, b, c) = (rucksacks[i * 3], rucksacks[i * 3 + 1], rucksacks[i * 3 + 2]);
                var r = (int)a.First(x => b.Contains(x) && c.Contains(x));
                var p = r >= (int)'a' ? r - (int)'a' + 1 : r - (int)'A' + 27;
                score += p;
            }
            return score.ToString();
        }
    }
}