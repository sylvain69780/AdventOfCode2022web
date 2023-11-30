using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.GrovePositioningSystem
{
    public class GrovePositioningSystemPart2Strategy : IPuzzleStrategy<GrovePositioningSystemModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(GrovePositioningSystemModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            const long decryptionKey = 811589153;
            var arrangement = model.Arrangement!.Select(x => (x.Id, x.Number * decryptionKey)).ToList();
            for (var turns = 0; turns < 10; turns++)
                GrovePositioningSystemModel.Mix(arrangement);
            var groveCoordinates = GrovePositioningSystemModel.DecodeGroveCoordinates(arrangement);
            yield return updateContext();
            provideSolution(groveCoordinates.ToString());
        }
    }
}
