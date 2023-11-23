using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sudoku
{
    public class SudokuModel : IPuzzleModel
    {
        public string PuzzleState { get; set; } = string.Empty;
        public Stack<string>? DFS;

        public void Parse(string input)
        {
           PuzzleState = input.Replace("\n", "").Replace("\r", "");
        }

        private const string Digits = "123456789";
        public string Entropy(int position)
        {
            var res = new HashSet<char>();
            var (col, row) = (position % 9, position / 9);
            var square = (Column: col / 3, Row: row / 3);
            for (var i = 0; i < 9; i++)
            {
                res.Add(PuzzleState[i + 9 * row]);
                res.Add(PuzzleState[col + 9 * i]);
                res.Add(PuzzleState[i % 3 + square.Column * 3 + (i / 3 + square.Row * 3) * 9]);
            }
            res.Remove('.');
            return string.Concat(Digits.Where(d => !res.Contains(d)));
        }
    }
}
