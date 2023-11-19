using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class PuzzleBase
    {
        public string Message => _message;
        public int Step => _step;
        public ProcessingProgressModel ProgressInfo => _enumerator!.Current;
        public bool SolveStep() => _enumerator!.MoveNext();

        protected int _step = 0;
        protected string _message = string.Empty;
        protected ProcessingProgressModel MarkProgress() => new() { Step = _step++ };
        protected IEnumerator<ProcessingProgressModel>? _enumerator;
        protected abstract IEnumerable<ProcessingProgressModel> Solve();

        public PuzzleBase(string input)
        {
            _enumerator = Solve().GetEnumerator();
        }
    }
}
