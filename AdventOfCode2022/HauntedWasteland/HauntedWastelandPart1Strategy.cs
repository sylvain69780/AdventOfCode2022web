using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HauntedWasteland
{
    public class HauntedWastelandPart1Strategy : IPuzzleStrategy<HauntedWastelandModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(HauntedWastelandModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var instructions = model.Instructions!;
            var nodes = model.Nodes!;
            var currentNode = "AAA";
            var i = 0;
            var count = 0;
            while (currentNode != "ZZZ")
            {
                var (Left, Right) = nodes[currentNode];
                currentNode = instructions[i] == 'L' ? Left : Right;
                i = (i + 1) % instructions.Length;
                count++;
            }
            yield return updateContext();
            provideSolution(count.ToString());
        }
    }
}
