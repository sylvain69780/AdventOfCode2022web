using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LensLibrary
{
    public class LensLibraryModel : IPuzzleModel
    {
        string[]? _initializationSequence;
        public string[]? InitializationSequence => _initializationSequence;

        public void Parse(string input)
        {
            _initializationSequence = input.Replace("\r","").Split(",");
        }
    }
}
