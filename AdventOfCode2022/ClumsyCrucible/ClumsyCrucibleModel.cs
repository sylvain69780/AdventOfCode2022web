using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ClumsyCrucible
{
    public class ClumsyCrucibleModel : IPuzzleModel
    {
        string[]? _map;
        public string[]? Map => _map;
        public void Parse(string input)
        {
            _map = input.Replace("\r", "").Split("\n");

        }
    }
}
