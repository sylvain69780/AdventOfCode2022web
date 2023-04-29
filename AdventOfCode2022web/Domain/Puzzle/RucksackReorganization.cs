namespace AdventOfCode2022web.Domain.Puzzle
{
    public class RucksackReorganization : PuzzleSolver
    {
        private static string[] ToLines(string s) => s.Split("\n");
        private static string Format(int v) => v.ToString();

        protected override string Part1(string puzzleInput)
        {
            var score = 0;
            foreach (var rucksack in ToLines(puzzleInput))
            {
                var compartmentSize = rucksack.Length /2;
                var (compartmentA, compartmentB) = (rucksack.Substring(0, compartmentSize), rucksack.Substring(compartmentSize, compartmentSize));
                var shared = (int)compartmentA.First(x => compartmentB.Contains(x));
                var priority = shared >= (int)'a' ? shared - (int)'a' + 1 : shared - (int)'A' + 27;
                score += priority;
            }
            return Format(score);
        }

        protected override string Part2(string puzzleInput)
        {
            var score = 0;
            var rucksacks = ToLines(puzzleInput);
            for (var i = 0; i < rucksacks.Length / 3; i++)
            {
                var (a, b, c) = (rucksacks[i * 3], rucksacks[i * 3 + 1], rucksacks[i * 3 + 2]);
                var badge = (int)a.First(x => b.Contains(x) && c.Contains(x));
                var priority = badge >= (int)'a' ? badge - (int)'a' + 1 : badge - (int)'A' + 27;
                score += priority;
            }
            return Format(score);
        }
    }
}