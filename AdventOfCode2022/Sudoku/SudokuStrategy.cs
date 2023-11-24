using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sudoku
{
    public class SudokuStrategy : IPuzzleStrategy<SudokuModel>
    {
        public string Name { get; set; } = "Solution 1";
        public IEnumerable<ProcessingProgressModel> GetSteps(SudokuModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            model.DFS = new Stack<string>();
            model.DFS.Push(model.PuzzleState);
            bool puzzleCompleted = false;
            while (!puzzleCompleted && model.DFS.TryPop(out var puzzleState))
            {
                model.PuzzleState = puzzleState;
                var emptySlots = Enumerable.Range(0, 9 * 9).Where(x => puzzleState[x] == '.').ToArray();
                if (emptySlots.Length == 0)
                    puzzleCompleted = true;
                else
                {
                    var slotsWithMinimalEntropy = emptySlots.Select(x => (p: x, e: model.Entropy(x))).OrderBy(x => x.e.Length).ThenBy(x => x.p).ToArray();
                    var slot = slotsWithMinimalEntropy[0];
                    if (slot.e == string.Empty)
                    {
                        yield return updateContext();
                        continue;
                    }
                    var sb = new StringBuilder(puzzleState);
                    for (var i = 0; i < slot.e.Length; i++)
                    {
                        sb[slot.p] = slot.e[i];
                        model.DFS.Push(sb.ToString());
                    }
                }
            }
            if (puzzleCompleted)
                provideSolution(FormatPuzzleState(model.PuzzleState));
            else
                provideSolution("No solution is found for this Sudoku !");
        }


        private string FormatPuzzleState(string puzzleState)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < 9; i++)
            {
                sb.Append(puzzleState[(i * 9)..((i + 1) * 9)] + "\n");
            }
            return sb.ToString();
        }

    }
}
