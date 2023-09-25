using AdventOfCode2022Solutions.PuzzleSolutions;
using System.Globalization;

namespace AdventOfCode2022Solutions.PuzzleSolutions.BoilingBoulders
{
    public class BoilingBouldersSolution : IPuzzleSolutionIter
    {
        private List<Voxel>? Voxels;

        public void Initialize(string puzzleInput)
        {
            Voxels = puzzleInput.Split("\n").Select(x => x.Split(','))
                .Select(x => x.Select(y => int.Parse(y)).ToArray())
                .Select(x => new Voxel(X: x[0], Y: x[1], Z: x[2])).ToList();
        }

        private static readonly List<Voxel> CubeFaces = new()
        {
                new Voxel(1,0,0),
                new Voxel(-1,0,0),
                new Voxel(0,1,0),
                new Voxel(0,-1,0),
                new Voxel(0,0,1),
                new Voxel(0,0,-1)
            };

        public IEnumerable<string> SolveFirstPart()
        {
            var map = Voxels!.ToHashSet();
            var CountOfFacesNotConnected = 0;
            foreach (var cube in Voxels!)
            {
                foreach (var face in CubeFaces)
                {
                    if (!map.Contains(cube.Plus(face)))
                        CountOfFacesNotConnected++;
                }
            }
            yield return CountOfFacesNotConnected.ToString();
        }
        public IEnumerable<string> SolveSecondPart()
        {
            var dropletVoxels = Voxels!.ToHashSet();
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
                foreach (var cubeFace in CubeFaces)
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
                foreach (var face in CubeFaces)
                {
                    var newPoint = point.Plus(face);
                    if (steamParticules.Contains(newPoint) && !dropletVoxels.Contains(newPoint))
                    {
                        exteriorSurfaceArea++;
                    }
                }
            }
            yield return exteriorSurfaceArea.ToString();
        }
    }

    public record struct Voxel(int X, int Y, int Z);

    public record struct RangeOfCoordinates(Voxel LowerCoordinates, Voxel HigherCoordinates);

    public static class PointExtensions
    {
        public static Voxel Plus(this Voxel a, Voxel b)
            => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static bool IsOutOfRange(this RangeOfCoordinates range, Voxel p)
            => p.X > range.HigherCoordinates.X || p.X < range.LowerCoordinates.X
            || p.Y > range.HigherCoordinates.Y || p.Y < range.LowerCoordinates.Y
            || p.Z > range.HigherCoordinates.Z || p.Z < range.LowerCoordinates.Z;
    }

    public static class DoubleExtensions
    {
        public static string ToStringCSS(this double value)
        {
            return value.ToString(CultureInfo.GetCultureInfo("en-GB"));
        }
    }
}