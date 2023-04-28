namespace AdventOfCode2022web.Domain.Puzzle
{
    public class RucksackReorganization : IPuzzleSolver
    {
        public async IAsyncEnumerable<string> Part1Async(string input)
        {
            Input = input;
            yield return Part1();
            await Task.Delay(1);
        }
        public async IAsyncEnumerable<string> Part2Async(string input)
        {
            Input = input;
            yield return Part2();
            await Task.Delay(1);
        }

        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            var score = 0;
            foreach (var rucksack in Input.Split("\n"))
            {
                var l = rucksack.Length;
                var (a, b) = (rucksack.Substring(0, l / 2), rucksack.Substring(l / 2, l / 2));
                var r = (int)a.First(x => b.Contains(x));
                var p = r >= (int)'a' ? r - (int)'a' + 1 : r - (int)'A' + 27;
                score += p;
            }
            return score.ToString();
        }
        public string Part2()
        {
            var score = 0;
            var rucksacks = Input.Split("\n");
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