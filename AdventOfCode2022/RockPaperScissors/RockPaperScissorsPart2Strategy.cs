namespace Domain.RockPaperScissors
{
    public class RockPaperScissorsPart2Strategy : IPuzzleStrategy<RockPaperScissorsModel>
    {
        public string Name { get; set; } = "Part 2";
        /// <summary>Decodes Input PART 2 X = Lose Y = Draw Z = Win</summary>
        /// <param name="s"></param>
        /// <returns>OpponentPlayed ExpectedResult tuple</returns>
        private static (Moves OpponentPlayed, GameResults ExpectedResult) DecodeMovesPart2(string s)
            => ((Moves)(s[0] - 'A'), (GameResults)(s[2] - 'X'));

        public IEnumerable<ProcessingProgressModel> GetSteps(RockPaperScissorsModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            model.Score = 0;
            foreach (var (opponentPlayed, expectedResult) in model.RoundsPlayed.Select(x => DecodeMovesPart2(x)))
            {
                var youPlay = opponentPlayed; // Draw
                if (expectedResult == GameResults.Win)
                    youPlay = Rules.WinningCases.Find(x => x.SecondMove == opponentPlayed).FirstMove;
                if (expectedResult == GameResults.Lose)
                    youPlay = Rules.WinningCases.Find(x => x.FirstMove == opponentPlayed).SecondMove;
                model.Score += (int)youPlay + 1;
                model.Score += (int)expectedResult * 3;
                yield return updateContext();
            }
            provideSolution(model.Score.ToString());
        }
    }
}
