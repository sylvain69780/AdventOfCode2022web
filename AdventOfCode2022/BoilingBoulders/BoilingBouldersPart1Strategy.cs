using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BoilingBoulders
{
    public class BoilingBouldersPart1Strategy : IPuzzleStrategy<BoilingBouldersModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(BoilingBouldersModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var map = model.Voxels!.ToHashSet();
            var CountOfFacesNotConnected = 0;
            foreach (var cube in model.Voxels!)
            {
                foreach (var face in BoilingBouldersModel.CubeFaces)
                {
                    if (!map.Contains(cube.Plus(face)))
                        CountOfFacesNotConnected++;
                }
            }
            yield return updateContext();
            provideSolution(CountOfFacesNotConnected.ToString());
        }
    }
}
