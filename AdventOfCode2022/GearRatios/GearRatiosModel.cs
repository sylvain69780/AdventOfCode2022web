using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.GearRatios
{
    public partial class GearRatiosModel : IPuzzleModel
    {
        string[]? _engineSchematic;
        List<(int x, int y, string val)> _partNumbers = new();
        List<(int x, int y, char symbol)> _symbols = new();
        public string[]? EngineSchematic => _engineSchematic;
        public List<(int x, int y, string val)> PartNumbers => _partNumbers;
        public List<(int x, int y, char symbol)> Symbols => _symbols;
        public void Parse(string input)
        {
            _engineSchematic = input.Replace("\r", "").Split("\n");
            _partNumbers.Clear();
            _symbols.Clear();
            var regexp = PartNumber();
            for (var y = 0; y < _engineSchematic.Length; y++)
            {
                var line = _engineSchematic[y];
                for (var x = 0; x < line.Length; x++)
                    if (line[x] != '.' && (line[x] > '9' || line[x] < '0'))
                        _symbols.Add((x, y, line[x]));
                var matches = regexp.Matches(line);
                for (var m = 0; m < matches.Count; m++)
                {
                    var col = matches[m];
                    _partNumbers.Add((col.Index, y, col.Value));
                }
            }
        }

        [GeneratedRegex("\\d+")]
        private static partial Regex PartNumber();

        public static int Distance((int x, int y, int l) a, (int x, int y) b)
        {
            var ba = b.x - a.x;
            return Math.Max(
                Math.Abs(b.y - a.y),
                Math.Max(ba - a.l + 1, -ba)
                );
        }
    }
}
