namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(2, "Rock Paper Scissors")]
    public class RockPaperScissors : IPuzzleSolver
    {
        private static string[] ToLines(string s) => s.Split("\n");
        private static string Format(int v) => v.ToString();

        private enum Moves { Rock = 0, Paper = 1, Scissors = 2 };
        private enum GameResults { Lose = 0, Draw = 1, Win = 2 };

        /// <summary>
        /// Rules of the Game : When the round is lost 
        /// </summary>
        private static readonly List<(Moves FirstMove, Moves SecondMove)> FirstPlayerWins = new() {
                (Moves.Rock, Moves.Scissors),
                (Moves.Scissors, Moves.Paper),
                (Moves.Paper,Moves.Rock)
            };

        /// <summary>Decodes Input PART 1 A = X = Rock B = Y = Paper C = Z = Scissors</summary>
        /// <param name="s"></param>
        /// <returns>OpponentPlayed YouPlayed tuple</returns>
        private static (Moves OpponentPlayed, Moves YouPlayed) DecodeMovesPart1(string s)
            => ((Moves)(s[0] - 'A'), (Moves)(s[2] - 'X'));

        /// <summary>Decodes Input PART 2 X = Lose Y = Draw Z = Win</summary>
        /// <param name="s"></param>
        /// <returns>OpponentPlayed ExpectedResult tuple</returns>
        private static (Moves OpponentPlayed, GameResults ExpectedResult) DecodeMovesPart2(string s)
            => ((Moves)(s[0] - 'A'), (GameResults)(s[2] - 'X'));

        public IEnumerable<string> SolveFirstPart(string puzzleInput)
        {
            var score = 0;
            foreach (var round in ToLines(puzzleInput).Select(x => DecodeMovesPart1(x)))
            {
                score += (int)round.YouPlayed + 1;
                if (round.YouPlayed == round.OpponentPlayed)
                    score += 3;
                else if (!FirstPlayerWins.Contains(round))
                    score += 6;
            }
            yield return Format(score);
        }

        public IEnumerable<string> SolveSecondPart(string puzzleInput)
        {
            var score = 0;
            foreach (var (opponentPlayed, expectedResult) in ToLines(puzzleInput).Select(x => DecodeMovesPart2(x)))
            {
                var youPlay = opponentPlayed; // Draw
                if (expectedResult == GameResults.Win)
                    youPlay = FirstPlayerWins.Find(x => x.SecondMove == opponentPlayed).FirstMove;
                if (expectedResult == GameResults.Lose)
                    youPlay = FirstPlayerWins.Find(x => x.FirstMove == opponentPlayed).SecondMove;
                score += (int)youPlay + 1;
                score += (int)expectedResult * 3;
            }
            yield return Format(score);
        }
    }
}