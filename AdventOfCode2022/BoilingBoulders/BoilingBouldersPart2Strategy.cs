using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BoilingBoulders
{
    public class BoilingBouldersPart2Strategy : IPuzzleStrategy<BoilingBouldersModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(BoilingBouldersModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var dropletVoxels = model.Voxels!.ToHashSet();
            var rangeOfCoordinates = new RangeOfCoordinates(
                LowerCoordinates: new Voxel(dropletVoxels.Min(p => p.X) - 1, dropletVoxels.Min(p => p.Y) - 1, dropletVoxels.Min(p => p.Z) - 1),
                HigherCoordinates: new Voxel(dropletVoxels.Max(p => p.X) + 1, dropletVoxels.Max(p => p.Y) + 1, dropletVoxels.Max(p => p.Z) + 1)
                );
            var queue = new Queue<Voxel>();
            queue.Enqueue(rangeOfCoordinates.LowerCoordinates);
            var steamParticules = new HashSet<Voxel>();
            while (queue.Count > 0)
            {
                var steamParticule = queue.Dequeue();
                foreach (var cubeFace in BoilingBouldersModel.CubeFaces)
                {
                    var expandedSteamParticule = steamParticule.Plus(cubeFace);
                    if (
                        rangeOfCoordinates.IsOutOfRange(expandedSteamParticule)
                        || steamParticules.Contains(expandedSteamParticule)
                        || dropletVoxels.Contains(expandedSteamParticule))
                        continue;
                    steamParticules.Add(expandedSteamParticule);
                    queue.Enqueue(expandedSteamParticule);
                }
            }

            var exteriorSurfaceArea = 0;
            foreach (var point in dropletVoxels)
            {
                foreach (var face in BoilingBouldersModel.CubeFaces)
                {
                    var newPoint = point.Plus(face);
                    if (steamParticules.Contains(newPoint) && !dropletVoxels.Contains(newPoint))
                    {
                        exteriorSurfaceArea++;
                    }
                }
            }
            yield return updateContext();
            provideSolution(exteriorSurfaceArea.ToString());
        }
    }
}
