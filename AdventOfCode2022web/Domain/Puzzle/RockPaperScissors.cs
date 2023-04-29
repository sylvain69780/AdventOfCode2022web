using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2022web.Domain.Puzzle
{
    public class RockPaperScissors : PuzzleSolver
    {
        private static string[] ToLines(string s) => s.Split("\n");
        private enum Moves { Rock = 0, Paper = 1, Scissors = 2 };
        private enum GameResults { Lose = 0, Draw = 1, Win = 2 };
        // Rules
        private static readonly (Moves OpponentPlayed, Moves YouPlayed)[] RoundIsLost = {
                (Moves.Rock, Moves.Scissors),
                (Moves.Scissors, Moves.Paper),
                (Moves.Paper,Moves.Rock)
            };
        // Decodes Input PART 1
        // A = X = Rock 
        // B = Y = Paper 
        // C = Z = Scissors 
        private static (Moves OpponentPlayed, Moves YouPlayed) Decode1(string s) 
            => ((Moves)(s[0] - 'A'),(Moves)(s[2] - 'X'));
        // Decodes Input PART 2
        // X = Lose
        // Y = Draw
        // Z = Win
        private static (Moves OpponentPlayed, GameResults ExpectedResult) Decode2(string s)
            => ((Moves)(s[0] - 'A'), (GameResults)(s[2] - 'X'));
        private static string Format(int v) => v.ToString();
        protected override string Part1(string puzzleInput)
        {
            var score = 0;
            foreach (var round in ToLines(puzzleInput).Select(x => Decode1(x)))
            {
                score += (int)round.YouPlayed + 1;
                if (!RoundIsLost.Contains(round))
                    score += round.YouPlayed == round.OpponentPlayed ? 3 : 6;
            }
            return Format(score);
        }
        protected override string Part2(string puzzleInput)
        {
            var score = 0;
            foreach (var round in ToLines(puzzleInput).Select(x => Decode2(x)))
            {
                var youPlay = round.OpponentPlayed;
                if (round.ExpectedResult == GameResults.Lose)
                    youPlay = RoundIsLost.First(x => x.OpponentPlayed == round.OpponentPlayed).YouPlayed;
                if (round.ExpectedResult == GameResults.Win)
                    youPlay = RoundIsLost.First(x => x.YouPlayed == round.OpponentPlayed).OpponentPlayed;
                score += (int)youPlay + 1;
                score += (int)round.ExpectedResult * 3;
            }
            return Format(score);
        }
    }
}