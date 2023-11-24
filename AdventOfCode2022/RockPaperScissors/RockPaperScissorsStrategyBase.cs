using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RockPaperScissors
{
    abstract public class RockPaperScissorsStrategyBase : IPuzzleStrategy<RockPaperScissorsModel>
    {
        public abstract string Name { get; set; }

        public abstract IEnumerable<ProcessingProgressModel> GetSteps(RockPaperScissorsModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution);

        public static readonly List<(Moves FirstMove, Moves SecondMove)> WinningCases = new() {
                (Moves.Rock, Moves.Scissors),
                (Moves.Scissors, Moves.Paper),
                (Moves.Paper,Moves.Rock)
            };
    }
}
