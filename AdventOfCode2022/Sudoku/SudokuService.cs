using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sudoku
{
    public class SudokuService : SimplePuzzleService<SudokuModel>
    {
        public SudokuService(IPuzzleStrategy<SudokuModel> strategy) : base(strategy)
        {
        }

        public string PuzzleState => _model.PuzzleState;

        public Stack<string>? DFS => _model.DFS;

        public string Entropy(int position) => _model.Entropy(position);
    }
}
