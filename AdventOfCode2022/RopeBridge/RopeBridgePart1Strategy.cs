using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RopeBridge
{
    public class RopeBridgePart1Strategy : IPuzzleStrategy<RopeBridgeModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(RopeBridgeModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var visitedPositions = new HashSet<(int x, int y)>();
            var head = (x: 0, y: 0);
            var tail = (x: 0, y: 0);
            visitedPositions.Add(tail);
            foreach (var move in model.SeriesOfMotions!)
            {
                var (x, y) = RopeBridgeModel.Directions[move];
                head.x += x;
                head.y += y;
                tail = RopeBridgeModel.MoveTailPosition(tail, head);
                visitedPositions.Add(tail);
            }
            yield return updateContext();
            provideSolution(visitedPositions.Count.ToString());
        }
    }
}
