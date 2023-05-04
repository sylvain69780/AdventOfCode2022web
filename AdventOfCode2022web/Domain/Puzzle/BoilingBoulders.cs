namespace AdventOfCode2022web.Domain.Puzzle
{
    [Puzzle(18, "Boiling Boulders")]
    public class BoilingBoulders : IPuzzleSolver
    {
        public IEnumerable<string> SolveFirstPart(string inp)
        {
            var input = inp.Split("\n").Select(x => x.Split(','))
                .Select(x => x.Select(y => int.Parse(y)).ToArray())
                .Select(x => (x: x[0], y: x[1], z: x[2]));

            var grid = input.ToHashSet();
            var faces = new List<(int, int, int)>
            {
                (1,0,0),
                (-1,0,0),
                (0,1,0),
                (0,-1,0),
                (0,0,1),
                (0,0,-1)
            };
            var score = 0;
            foreach (var p in input)
            {
                foreach (var f in faces)
                {
                    if (!grid.Contains((p.x + f.Item1, p.y + f.Item2, p.z + f.Item3)))
                        score++;
                }
            }
            yield return score.ToString();
        }
        public IEnumerable<string> SolveSecondPart(string inp)
        {
            var input = inp.Split("\n").Select(x => x.Split(','))
                .Select(x => x.Select(y => int.Parse(y)).ToArray())
                .Select(x => (x: x[0], y: x[1], z: x[2]));

            var grid = input.ToHashSet();
            var faces = new List<(int x, int y, int z)>
            {
                (1,0,0),
                (-1,0,0),
                (0,1,0),
                (0,-1,0),
                (0,0,1),
                (0,0,-1)
            };

            var minX = input.Select(x => x.x).Min() - 1;
            var minY = input.Select(x => x.y).Min() - 1;
            var minZ = input.Select(x => x.z).Min() - 1;
            var maxX = input.Select(x => x.x).Max() + 1;
            var maxY = input.Select(x => x.y).Max() + 1;
            var maxZ = input.Select(x => x.z).Max() + 1;
            var start = (minX, minY, minZ);
            var queue = new Queue<(int x, int y, int z)>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                var (x, y, z) = queue.Dequeue();
                foreach (var f in faces)
                {
                    var (nx, ny, nz) = (x + f.x, y + f.y, z + f.z);
                    if (nx > maxX || nx < minX || ny > maxY || ny < minY || nz > maxZ || nz < minZ || grid.Contains((nx, ny, nz)) || input.Contains((nx, ny, nz)))
                        continue;
                    grid.Add((nx, ny, nz));
                    queue.Enqueue((nx, ny, nz));
                }
            }

            var score = 0;
            foreach (var p in input)
            {
                foreach (var f in faces)
                {
                    var pp = (p.x + f.x, p.y + f.y, p.z + f.z);
                    if (grid.Contains(pp) && !input.Contains(pp))
                    {
                        score++;
                        Console.WriteLine(score);
                        yield return score.ToString();
                    }
                }
            }
            Console.WriteLine(score);
            yield return score.ToString();
        }
    }
}