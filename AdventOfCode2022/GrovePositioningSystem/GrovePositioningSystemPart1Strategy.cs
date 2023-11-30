using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.GrovePositioningSystem
{
    public class GrovePositioningSystemPart1Strategy : IPuzzleStrategy<GrovePositioningSystemModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(GrovePositioningSystemModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            GrovePositioningSystemModel.Mix(model.Arrangement!);
            var groveCoordinates = GrovePositioningSystemModel.DecodeGroveCoordinates(model.Arrangement);
            yield return updateContext();
                provideSolution(groveCoordinates.ToString());
        }
    }
}
