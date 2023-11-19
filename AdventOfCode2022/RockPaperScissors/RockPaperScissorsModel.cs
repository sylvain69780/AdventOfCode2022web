using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RockPaperScissors
{
    public enum Moves { Rock = 0, Paper = 1, Scissors = 2 };
    public enum GameResults { Lose = 0, Draw = 1, Win = 2 };
    public class RockPaperScissorsModel : IPuzzleModel
    {
        string[] _roundsPlayed = Array.Empty<string>();
        public int Score { get; set; }
        public IEnumerable<string> RoundsPlayed => _roundsPlayed;
        public void Parse(string input)
        {
            Score = 0;
            _roundsPlayed = input.Split("\n");
        }
    }
}
