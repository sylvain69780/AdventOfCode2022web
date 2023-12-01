using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Trebuchet
{
    public class TrebuchetModel : IPuzzleModel
    {
        string[]? _input;
        public string[]? Input => _input;
        public void Parse(string input)
        {
            _input = input.Replace("\r","").Split("\n");
        }
    }
}
