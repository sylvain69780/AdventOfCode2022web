namespace AdventOfCode2022web.Domain.Puzzle
{
    public class RockPaperScissors : PuzzleSolver
    {
        protected override string Part1(string input)
        {
            var defeats = new List<(int, int)> { (1, 3), (3, 2), (2, 1) };
            var score = 0;
            foreach (var (opponent, you) in input.Split("\n").Select(x => x.Split(" ")).Select(x => ("ABC".IndexOf(x[0][0]) + 1, "XYZ".IndexOf(x[1][0]) + 1)))
            {
                score += you;
                if (!defeats.Contains((opponent, you)))
                    score += you == opponent ? 3 : 6;
            }
            return score.ToString();
        }
        protected override string Part2(string input)
        {
            var score = 0;
            var defeats = new List<(int, int)> { (1, 3), (3, 2), (2, 1) };
            foreach (var (opponent, ending) in input.Split("\n").Select(x => x.Split(" ")).Select(x => ("ABC".IndexOf(x[0][0]) + 1, "XYZ".IndexOf(x[1][0]) + 1)))
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