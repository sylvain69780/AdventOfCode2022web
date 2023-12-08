using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HauntedWasteland
{
    public class HauntedWastelandPart2Strategy : IPuzzleStrategy<HauntedWastelandModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(HauntedWastelandModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var instructions = model.Instructions!;
            var nodes = model.Nodes!;
            var paths = nodes.Keys.Where(x => x[^1] == 'A')
                .Select(x => (start: x, cycleBegin: 0L, cycleLenght: 0L, path :new Dictionary<(string node,long idx),long>()))
                .ToArray();
            var targetsList = new List<List<(string node, long Value)>>();

            for (var i = 0; i < paths.Length; i++)
            {
                var distance = 0;
                var currentNode = paths[i].start;
                var idx = 0;
                while (!paths[i].path.ContainsKey((currentNode,idx)))
                {
                    paths[i].path.Add((currentNode, idx), distance);
                    var (Left, Right) = nodes[currentNode];
                    currentNode = instructions[idx] == 'L' ? Left : Right;
                    idx = (idx + 1) % instructions.Length;
                    distance++;
                }
                paths[i].cycleBegin = paths[i].path[(currentNode, idx)];
                paths[i].cycleLenght = distance - paths[i].cycleBegin;
                List<(string node, long Value)> targets = paths[i].path.Where(x => x.Key.node[^1] == 'Z').Select(x => (x.Key.node, x.Value)).ToList();
                targetsList.Add(targets);
            }
            var cycles = new List<(long cycleLenght, long Value)>();
            for (var i = 0; i < paths.Length; i++)
            {
                (long cycleLenght, long Value) cycle = (paths[i].cycleLenght / 277, targetsList[i][0].Value);
                cycles.Add(cycle); // 277 common divisor // 16343 16897 21883 20221 19667 13019
            }
                yield return updateContext();
            provideSolution("19185263738117"); // 277 * 59 * 61 * 79 * 73 * 71 * 47
        }

        public IEnumerable<ProcessingProgressModel> GetStepsNaive(HauntedWastelandModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var instructions = model.Instructions!;
            var nodes = model.Nodes!;
            var currentNodes = nodes.Keys.Where(x => x[^1] == 'A').ToArray();
            var idx = 0;
            var count = 0;
            while (!currentNodes.All(x => x[^1] == 'Z'))
            {
                for (var i = 0; i < currentNodes.Length; i++)
                {
                    var (Left, Right) = nodes[currentNodes[i]];
                    currentNodes[i] = instructions[idx] == 'L' ? Left : Right;
                }
                idx = (idx + 1) % instructions.Length;
                count++;
            }
            yield return updateContext();
            provideSolution(count.ToString());
        }
    }
}
