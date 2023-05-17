
namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(18, "Boiling Boulders")]
    public class BoilingBoulders : IPuzzleSolver
    {
        private static List<Point> GetVoxels(string puzzleInput)
        {
            return puzzleInput.Split("\n").Select(x => x.Split(','))
                .Select(x => x.Select(y => int.Parse(y)).ToArray())
                .Select(x => new Point(X: x[0],Y: x[1],Z: x[2])).ToList();
        }
        private static readonly List<Point> CubeFaces = new()
        {
                new Point(1,0,0),
                new Point(-1,0,0),
                new Point(0,1,0),
                new Point(0,-1,0),
                new Point(0,0,1),
                new Point(0,0,-1)
            };

        public string SolveFirstPart(string puzzleInput)
        {
            var voxels = GetVoxels(puzzleInput);
            var map = voxels.ToHashSet();
            var CountOfFacesNotConnected = 0;
            foreach (var cube in voxels)
            {
                foreach (var face in CubeFaces)
                {
                    if (!map.Contains(new Point(cube.X + face.X, cube.Y + face.Y, cube.Z + face.Z)))
                        CountOfFacesNotConnected++;
                }
            }
            return CountOfFacesNotConnected.ToString();
        }
        public string SolveSecondPart(string puzzleInput)
        {
            var voxels = GetVoxels(puzzleInput);
            var exteriorPoints = voxels.ToHashSet();

            var (pMin, pMax) = (
                new Point(voxels.Min(p => p.X) - 1, voxels.Min(p => p.Y) - 1, voxels.Min(p => p.Z) - 1),
                new Point(voxels.Max(p => p.X) + 1, voxels.Max(p => p.Y) + 1, voxels.Max(p => p.Z) + 1)
                );
            var queue = new Queue<Point>();
            queue.Enqueue(pMin);
            while (queue.Count > 0)
            {
                var point = queue.Dequeue();
                foreach (var cubeFace in CubeFaces)
                {
                    var newPoint = new Point(point.X + cubeFace.X, point.Y + cubeFace.Y, point.Z + cubeFace.Z);
                    if (newPoint.X > pMax.X || newPoint.X < pMin.X || newPoint.Y > pMax.Y || newPoint.Y < pMin.Y || newPoint.Z > pMax.Z || newPoint.Z < pMin.Z || exteriorPoints.Contains(newPoint) || voxels.Contains(newPoint))
                        continue;
                    exteriorPoints.Add(newPoint);
                    queue.Enqueue(newPoint);
                }
            }

            var exteriorSurfaceArea = 0;
            foreach (var point in voxels)
            {
                foreach (var face in CubeFaces)
                {
                    var newPoint = new Point(point.X + face.X, point.Y + face.Y, point.Z + face.Z);
                    if (exteriorPoints.Contains(newPoint) && !voxels.Contains(newPoint))
                    {
                        exteriorSurfaceArea++;
                        Console.WriteLine(exteriorSurfaceArea);
                    }
                }
            }
            Console.WriteLine(exteriorSurfaceArea);
            return exteriorSurfaceArea.ToString();
        }
    }
}