using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TheFloorWillBeLava
{
    public class TheFloorWillBeLavaModel : IPuzzleModel
    {
        string[]? _layout;
        public string[]? Layout => _layout;
        public void Parse(string input)
        {
            _layout = input.Replace("\r", "").Split("\n");
        }
    }
}
