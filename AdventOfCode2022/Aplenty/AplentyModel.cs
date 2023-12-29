using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Aplenty
{
    public partial class AplentyModel : IPuzzleModel
    {

        public (string name, List<(string name, string oper, int amount, string applyRule)> rules)[]? Workflows => _workflows;

        public (int x, int m, int a, int s)[]? Ratings => _ratings;

        (string name, List<(string name, string oper, int amount, string applyRule)> rules)[]? _workflows;
        (int x, int m, int a, int s)[]? _ratings;
        public void Parse(string input)
        {
            var inp = input.Replace("\r", "").Split("\n\n");
            _workflows = inp[0].Split("\n")
                .Select(x => WorkflowRegex().Match(x))
                .Select(x =>
                {
                    var name = x.Groups[1].Value;
                    var rulesStr = x.Groups[2].Value.Split(",");
                    var rules = new List<(string name, string oper, int amount, string applyRule)>();
                    foreach (var ruleStr in rulesStr)
                    {
                        var r = RuleRegex().Match(ruleStr);
                        if (r.Success)
                            rules.Add((r.Groups[1].Value, r.Groups[2].Value, int.Parse(r.Groups[3].Value), r.Groups[4].Value));
                        else
                            rules.Add((string.Empty, string.Empty, 0, ruleStr));
                    }
                    return (name, rules);
                })
                .ToArray();

            _ratings = inp[1].Split("\n").Select(x => RatingRegex().Match(x))
                            .Select(v => (
                                x: int.Parse(v.Groups[1].Value),
                                m: int.Parse(v.Groups[2].Value),
                                a: int.Parse(v.Groups[3].Value),
                                s: int.Parse(v.Groups[4].Value)
                                ))
                            .ToArray();
        }

        [GeneratedRegex("([a-zA-Z]+)\\{(.+)\\}")]
        private static partial Regex WorkflowRegex();
        [GeneratedRegex("([a-zA-Z]+)([><=])(\\d+):([a-zA-Z]+)")]
        private static partial Regex RuleRegex();
        [GeneratedRegex("{x=(\\d+),m=(\\d+),a=(\\d+),s=(\\d+)}")]
        private static partial Regex RatingRegex();
    }
}
