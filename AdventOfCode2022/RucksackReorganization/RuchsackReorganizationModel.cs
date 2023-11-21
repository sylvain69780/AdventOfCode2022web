using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RucksackReorganization
{
    public class RuchsackReorganizationModel : IPuzzleModel
    {
        private string[]? _lines;
        public string[]? Lines => _lines;
        public void Parse(string input)
        {
            _lines = input.Split('\n');
        }
    }
}
