using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RopeBridge
{
    public class RopeBridgePart2Strategy : IPuzzleStrategy<RopeBridgeModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(RopeBridgeModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var visited = new HashSet<(int x, int y)>();
            var head = (x: 0, y: 0);
            var tails = new (int x, int y)[9];
            visited.Add((0, 0));

            foreach (var move in model.SeriesOfMotions!)
            {
                var (x, y) = RopeBridgeModel.Directions[move];
                head.x += x;
                head.y += y;
                var previous = head;
                foreach (var i in Enumerable.Range(0, 9))
                {
                    tails[i] = RopeBridgeModel.MoveTailPosition(tails[i], previous);
                    previous = tails[i];
                }
                visited.Add(tails[8]);
            }
            yield return updateContext();
            provideSolution( visited.Count.ToString());
        }
    }
}
