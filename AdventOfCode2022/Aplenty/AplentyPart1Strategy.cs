using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aplenty
{
    public class AplentyPart1Strategy : IPuzzleStrategy<AplentyModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(AplentyModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var workflows = model.Workflows!;
            var ratings = model.Ratings!;
            var solution = 0;
            foreach (var rating in ratings)
            {
                var pos = (workflowName: "in", index: 0);
                while (pos.workflowName != "A" && pos.workflowName != "R")
                {
                    var workflow = workflows.Single(x => x.name == pos.workflowName);
                    if (pos.index == workflow.rules.Count - 1)
                        pos = (workflow.rules[^1].applyRule, 0);
                    else
                    {
                        var (name, oper, amount, applyRule) = workflow.rules[pos.index];
                        var value = name switch
                        {
                            "s" => rating.s,
                            "a" => rating.a,
                            "m" => rating.m,
                            "x" => rating.x,
                            _ => throw new NotImplementedException(),
                        };
                        var testResult = oper == ">" ? value > amount : value < amount;
                        if (testResult)
                            pos = (applyRule, 0);
                        else
                            pos = (pos.workflowName, pos.index+1);
                    }
                }
                if (pos.workflowName == "A")
                    solution += rating.s + rating.a + rating.m + rating.x;
            }
            yield return updateContext();
            provideSolution(solution.ToString());
        }
    }
}
