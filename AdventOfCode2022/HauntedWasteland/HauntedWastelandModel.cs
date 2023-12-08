using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.HauntedWasteland
{
    public class HauntedWastelandModel : IPuzzleModel


    {
        string _instructions = string.Empty;
        public string Instructions => _instructions;
        Dictionary<string, (string Left, string Right)>? _nodes;
        public Dictionary<string, (string Left, string Right)>? Nodes => _nodes;
        public void Parse(string input)
        {
            var i = input.Replace("\r", "").Split("\n");
            _instructions = i[0];
            _nodes = i[2..].Select(x => Regex.Match(x, "([A-Z0-9]+) = \\(([A-Z0-9]+), ([A-Z0-9]+)\\)"))
                .Select(x => (Name: x.Groups[1].Captures[0].Value, Left: x.Groups[2].Captures[0].Value, Rigth: x.Groups[3].Captures[0].Value))
                .ToDictionary(x => x.Name, x => (x.Left, x.Rigth));

        }
    }
}
