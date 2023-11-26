using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SupplyStacks
{
    public class SupplyStacksPart2Strategy : IPuzzleStrategy<SupplyStacksModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(SupplyStacksModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var stacks = model.Stacks!;
            var moves = model.MovesToDo!;
            var tmp = new Stack<char>();
            foreach (var (count, from, to) in moves)
            {
                for (int i = 0; i < count; i++)
                    tmp.Push(stacks[from - 1].Pop());
                for (int i = 0; i < count; i++)
                    stacks[to - 1].Push(tmp.Pop());
                yield return updateContext();
            }
            provideSolution(string.Join("", stacks.Select(x => x.Count == 0 ? ' ' : x.Peek())));
        }
    }
}
