using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SupplyStacks
{
    public class SupplyStacksPart1Strategy : IPuzzleStrategy<SupplyStacksModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(SupplyStacksModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var stacks = model.Stacks!;
            var moves = model.MovesToDo!;
            foreach (var (count, from, to) in moves)
            {
                for (var i = 0; i < count; i++)
                {
                    var c = stacks[from - 1].Pop();
                    stacks[to - 1].Push(c);
                }
                yield return updateContext();
            }
            provideSolution(string.Join("", stacks.Select(x => x.Count == 0 ? ' ' : x.Peek())));
        }
    }
}
