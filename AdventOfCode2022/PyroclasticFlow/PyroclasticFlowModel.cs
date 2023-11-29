using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PyroclasticFlow
{
    public class PyroclasticFlowModel : IPuzzleModel
    {
        private string _puzzleInput = string.Empty;
        public string PuzzleInput => _puzzleInput;
        public void Parse(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }
    }
}
