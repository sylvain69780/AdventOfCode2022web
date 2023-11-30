using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MonkeyMap
{
    public class MonkeyMapPart2Strategy : IPuzzleStrategy<MonkeyMapModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(MonkeyMapModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            foreach (var (move, rotation) in model.Simulation!.Instructions!)
            {
                for (var step = 0; step < move; step++)
                {
                    var tmp = MonkeyMapModel.ComputeNextPositionOnCube(model.Simulation);
                    if (MonkeyMapModel.Map(model.Simulation, tmp.Position) != '#')
                        (model.Simulation.Position, model.Simulation.Direction) = tmp;
                    model.Simulation.Step++;
                }
                if (rotation == "R")
                    model.Simulation.Direction = (Direction)(((int)model.Simulation.Direction + 1) % 4);
                if (rotation == "L")
                    model.Simulation.Direction = (Direction)(((int)model.Simulation.Direction - 1 + 4) % 4);
            }
            yield return updateContext();
            provideSolution((1000 * (model.Simulation.Position.Y + 1) + 4 * (model.Simulation.Position.X + 1) + (int)model.Simulation.Direction).ToString());
        }
    }
}
