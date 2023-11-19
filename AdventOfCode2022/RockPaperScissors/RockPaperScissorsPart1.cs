namespace Domain.RockPaperScissors
{
    public class RockPaperScissorsPart1 : ISimplePuzzleStrategy<RockPaperScissorsModel>
    {
        /// <summary>Decodes Input PART 1 A = X = Rock B = Y = Paper C = Z = Scissors</summary>
        /// <param name="s"></param>
        /// <returns>OpponentPlayed YouPlayed tuple</returns>
        private (Moves OpponentPlayed, Moves YouPlayed) DecodeMovesPart1(string s)
            => ((Moves)(s[0] - 'A'), (Moves)(s[2] - 'X'));

        public IEnumerable<ProcessingProgressModel> GetSteps(RockPaperScissorsModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            foreach (var round in model.RoundsPlayed.Select(x => DecodeMovesPart1(x)))
            {
                model.Score += (int)round.YouPlayed + 1;
                if (round.YouPlayed == round.OpponentPlayed)
                    model.Score += 3;
                else if (!Rules.WinningCases.Contains(round))
                    model.Score += 6;
                yield return updateContext();
            }
            provideSolution(model.Score.ToString());
        }
    }
}
