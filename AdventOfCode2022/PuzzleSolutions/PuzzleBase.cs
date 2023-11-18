using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022Solutions.PuzzleSolutions
{
    public abstract class PuzzleBase
    {
        public string Message => _message;
        public int Step => _step;
        public ProgressInfo ProgressInfo => _enumerator!.Current;
        public bool SolveStep() => _enumerator!.MoveNext();

        protected int _step = 0;
        protected string _message = string.Empty;
        protected ProgressInfo MarkProgress() => new() { Step = _step++ };
        protected IEnumerator<ProgressInfo>? _enumerator;
        protected abstract IEnumerable<ProgressInfo> Solve();

        public PuzzleBase(string input)
        {
            _enumerator = Solve().GetEnumerator();
        }
    }
}
