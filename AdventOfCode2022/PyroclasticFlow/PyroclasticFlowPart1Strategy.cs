using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PyroclasticFlow
{
    public class PyroclasticFlowPart1Strategy : IPuzzleStrategy<PyroclasticFlowModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(PyroclasticFlowModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var jetGenerator = new JetGenerator { JetPattern = model.PuzzleInput };
            var rockGenerator = new RockGenerator();
            var occupiedSlots = new HashSet<(int x, int y)>();
            int highestPoint = 0;
            const int maxIterations = 2022;
            for (var i = 0; i <= maxIterations; i++)
            {
                var rock = rockGenerator.FetchRockShape();
                highestPoint = occupiedSlots.Count == 0 ? 0 : occupiedSlots.Select(p => p.y).Max() + 1;
                var rockPosition = (x: 2, y: highestPoint + 3);
                var stop = false;
                while (!stop)
                {
                    var direction = jetGenerator.FetchJetDirection() == '>' ? 1 : -1;
                    if (!rock.Select(x => (x.Item1 + rockPosition.x + direction, x.Item2 + rockPosition.y)).Any(x => x.Item1 < 0 || x.Item1 >= 7 || occupiedSlots.Contains(x)))
                        rockPosition.x += direction;
                    if (rock.Select(p => (x: p.x + rockPosition.x, y: p.y + rockPosition.y - 1)).Any(p => p.y < 0 || occupiedSlots.Contains(p)))
                        break;
                    else
                        rockPosition.y--;
                }
                foreach (var (x, y) in rock)
                    occupiedSlots.Add((x + rockPosition.x, y + rockPosition.y));

            }
            //Console.WriteLine(Visualize(occupiedSlots, highestPoint));
            yield return updateContext();
//            provideSolution($"After {maxIterations} rocks fallen, the Tower highest point is at y={highestPoint}");
            provideSolution($"{highestPoint}");
        }
    }
}
