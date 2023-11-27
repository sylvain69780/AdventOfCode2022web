using Domain.BlizzardBasin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HillClimbingAlgorithm
{
    public class HillClimbingAlgorithmPart2Strategy : IPuzzleStrategy<HillClimbingAlgorithmModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(HillClimbingAlgorithmModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            model.SetAsExplored(model.Start);
            var breadthFirstSearchQueue = new Queue<(int x, int y)>();
            foreach (var position in model.GetZeroHeighPositions())
                breadthFirstSearchQueue.Enqueue(position);
            var distance = 1;
            var newQueue = new Queue<(int, int)>();
            while (breadthFirstSearchQueue.TryDequeue(out var currentPosition))
            {
                foreach (var nextPosition in HillClimbingAlgorithmModel.Directions.Select(d => (currentPosition.x + d.x, currentPosition.y + d.y)))
                {
                    if (model.IsOutOfMap(nextPosition) || model.IsExploredPosition(nextPosition)
                        || model.Altitude(nextPosition) - model.Altitude(currentPosition) >= 2)
                        continue;
                    if (model.IsExit(nextPosition))
                    {
                        yield return updateContext();
                        provideSolution(distance.ToString());
                        yield break;
                    }
                    newQueue.Enqueue(nextPosition);
                    model.SetAsExplored(nextPosition);
                }
                if (breadthFirstSearchQueue.Count == 0)
                {
                    distance++;
                    while (newQueue.TryDequeue(out var item))
                        breadthFirstSearchQueue.Enqueue(item);
                }
            }
            provideSolution("Not found " + distance.ToString());
        }
    }
}
