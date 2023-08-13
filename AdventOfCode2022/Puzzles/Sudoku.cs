using System.Text;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(27, "Sudoku")]
    public class Sudoku : IPuzzleSolverV3
    {
        public void Setup(string input)
        {
            _puzzleInput = input.Replace("\n", "").Replace("\r", "");
            PuzzleState = _puzzleInput;
        }

        private string Entropy(int position)
        {
            var res = new HashSet<char>();
            var (col, row) = (position % 9, position / 9);
            var square = (Column :col / 3,Row:row / 3);
            for (var i = 0; i < 9; i++)
            {
                res.Add(PuzzleState[i + 9 * row]);
                res.Add(PuzzleState[col + 9 * i]);
                res.Add(PuzzleState[i % 3 + square.Column * 3 + (i / 3 + square.Row*3) * 9]);
            }
            res.Remove('.');
            return string.Concat(Digits.Where(d => !res.Contains(d)));
        }

        public IEnumerable<string> SolveFirstPart()
        {
            var DFS = new Stack<string>();
            DFS.Push(_puzzleInput);
            bool puzzleCompleted = false;
            while (!puzzleCompleted && DFS.TryPop(out PuzzleState!))
            {
                var emptySlots = Enumerable.Range(0, 9 * 9).Where(x => PuzzleState[x] == '.').ToArray();
                if (emptySlots.Length == 0)
                    puzzleCompleted = true;
                else
                {
                    var slotsWithMinimalEntropy = emptySlots.Select(x => (p: x, e: Entropy(x))).OrderBy(x => x.e.Length).ThenBy(x => x.p).ToArray();
                    var slot = slotsWithMinimalEntropy[0];
                    if (slot.e == string.Empty)
                        continue;
                    var sb = new StringBuilder(PuzzleState);
                    for (var i = 0; i<slot.e.Length;i++)
                    {
                        sb[slot.p] = slot.e[i];
                        DFS.Push(sb.ToString());
                    }
                }
                yield return FormatPuzzleState();
            }
        }
        public IEnumerable<string> SolveSecondPart()
        {
            yield return string.Empty;
        }

        private string FormatPuzzleState()
        {
            var sb = new StringBuilder();
            for (var i = 0; i<9; i++)
            {
                sb.Append(PuzzleState[(i * 9)..((i + 1) * 9)] + "\n");
            }
            return sb.ToString();
        }

        private string _puzzleInput = string.Empty;
        private const string Digits = "123456789";
        public string PuzzleState = string.Empty;
    }
}

