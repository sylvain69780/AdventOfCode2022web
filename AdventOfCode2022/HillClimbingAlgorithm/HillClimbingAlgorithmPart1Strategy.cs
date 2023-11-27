using Domain.BlizzardBasin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HillClimbingAlgorithm
{
    public class HillClimbingAlgorithmPart1Strategy : IPuzzleStrategy<HillClimbingAlgorithmModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(HillClimbingAlgorithmModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            model.SetAsExplored(model.Start);
            var breadthFirstSearchQueue = new Queue<(int x, int y)>();
            breadthFirstSearchQueue.Enqueue(model.Start);
            var score = 0;
            while (breadthFirstSearchQueue.Count > 0)
            {
                score++;
                var newQueue = new Queue<(int, int)>();
                while (breadthFirstSearchQueue.Count > 0)
                {
                    var currentPosition = breadthFirstSearchQueue.Dequeue();
                    foreach (var (x, y) in HillClimbingAlgorithmModel.Directions)
                    {
                        var nextPosition = (currentPosition.x + x, currentPosition.y + y);
                        if (model.IsOutOfMap(nextPosition) || model.IsExploredPosition(nextPosition))
                            continue;
                        if (model.Altitude(nextPosition) - model.Altitude(currentPosition) >= 2)
                            continue;
                        if (model.IsExit(nextPosition))
                        {
                            newQueue.Clear();
                            breadthFirstSearchQueue.Clear();
                            break;
                        }
                        newQueue.Enqueue(nextPosition);
                        model.SetAsExplored(nextPosition);
                    }
                }
                breadthFirstSearchQueue = newQueue;
            }
            yield return updateContext();
            provideSolution(score.ToString());

        }
    }
}
