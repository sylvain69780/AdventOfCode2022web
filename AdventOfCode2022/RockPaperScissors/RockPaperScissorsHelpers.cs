namespace Domain.RockPaperScissors
{
    internal static class Rules
    {

        /// <summary>
        /// Rules of the Game : When the round is lost 
        /// </summary>
        public static readonly List<(Moves FirstMove, Moves SecondMove)> WinningCases = new() {
                (Moves.Rock, Moves.Scissors),
                (Moves.Scissors, Moves.Paper),
                (Moves.Paper,Moves.Rock)
            };
    }
}