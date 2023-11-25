namespace Domain.Sudoku
{
    public class SudokuService : SimplePuzzleService<SudokuModel>
    {
        public SudokuService(IEnumerable<IPuzzleStrategy<SudokuModel>> strategies) : base(strategies)
        {
        }

        public string PuzzleState => _model.PuzzleState;

        public Stack<string>? DFS => _model.DFS;

        public string Entropy(int position) => _model.Entropy(position);
    }
}
