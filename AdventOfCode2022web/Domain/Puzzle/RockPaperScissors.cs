namespace AdventOfCode2022web.Domain.Puzzle
{
    public class RockPaperScissors : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            var defeats = new List<(int, int)> { (1, 3), (3, 2), (2, 1) };
            var score = 0;
            foreach (var (opponent, you) in Input.Split("\n").Select(x => x.Split(" ")).Select(x => ("ABC".IndexOf(x[0][0]) + 1, "XYZ".IndexOf(x[1][0]) + 1)))
            {
                score += you;
                if (!defeats.Contains((opponent, you)))
                    score += you == opponent ? 3 : 6;
            }
            return score.ToString();
        }
        public string Part2()
        {
            var score = 0;
            var defeats = new List<(int, int)> { (1, 3), (3, 2), (2, 1) };
            foreach (var (opponent, ending) in Input.Split("\n").Select(x => x.Split(" ")).Select(x => ("ABC".IndexOf(x[0][0]) + 1, "XYZ".IndexOf(x[1][0]) + 1)))
            {
                var you = opponent;
                if (ending == 1)
                    you = defeats.Find(x => x.Item1 == opponent).Item2;
                else if (ending == 3)
                    you = defeats.Find(x => x.Item2 == opponent).Item1;
                score += you;
                score += (ending - 1) * 3;
            }
            return score.ToString();
        }
    }
}