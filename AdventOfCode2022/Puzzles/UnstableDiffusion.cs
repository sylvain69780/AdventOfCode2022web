using System.Text;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(23, "Unstable Diffusion")]
    public class UnstableDiffusion : IPuzzleSolverV3
    {

        // public (int xMin, int yMin, int xMax, int yMax) ViewBox = (-4, -4, 4, 4);

        public (int X, int Y)[] Elves;
        public (int X, int Y)[] ElvesPrevPosition;

        private static readonly Dictionary<string, (int dx, int dy)> EightDirections = new()
        {
            {"N",(0,-1) },
            {"S",(0,1) },
            {"E",(1,0) },
            {"W",(-1,0) },
            {"NE",(1,-1) },
            { "SE",(1,1)},
            {"SW",(-1,1) },
            {"NW",(-1,-1) }
        };

        private static readonly (string Dir, List<string> Near)[] directionsToMoveIfNoElveNearby = new (string Dir, List<string> Near)[]
        {
            ("N", new List<string> {"N","NE","NW"} ),
            ( "S", new List<string> {"S","SE","SW"} ),
            ( "W", new List<string> {"W","NW","SW"} ),
            ( "E", new List<string> {"E","NE","SE"} )
        };

        public void Setup(string puzzleInput)
        {
            var input = puzzleInput.Split("\n");
            var row = 0;
            Elves = input
                .Select(x => (line: x, row: row++))
                .SelectMany(x => Enumerable.Range(0, x.line.Length).Where(col => x.line[col] == '#')
                .Select(col => (col, x.row)))
                .ToArray();
            ElvesPrevPosition = Elves.ToArray();
        }

        public string Visualize()
        {
            return string.Empty;
            //var xmin = ElvesPosition.Min(e => e.X);
            //var ymin = ElvesPosition.Min(e => e.Y);
            //var xmax = ElvesPosition.Max(e => e.X);
            //var ymax = ElvesPosition.Max(e => e.Y);
            //var sb = new StringBuilder();
            //for ( var y = ymin; y<=ymax; y++)
            //{
            //    for (var x = xmin; x <= xmax; x++) 
            //    {
            //        sb.Append(ElvesPosition.Contains((x, y)) ? '#' : '.');
            //    }
            //    sb.Append('\n');
            //}
            //return sb.ToString();
        }

        public IEnumerable<string> SolveFirstPart()
        {
            var rounds = 10;
            var directionIndex = 0;

            for (var round = 1; round <= rounds; round++)
            {
                var elvesPosition = Elves.ToHashSet();
                Array.Copy(Elves, ElvesPrevPosition, Elves.Length);
                var elvesCollisions = new Dictionary<(int X, int Y), int>();
                for (var id = 0; id < Elves.Length; id++)
                {
                    var elve = Elves[id];
                    var neighbourgs = EightDirections.Values
                        .Select(x => (elve.X + x.dx, elve.Y + x.dy))
                        .Where(x => elvesPosition.Contains(x)).ToList();
                    if (neighbourgs.Count == 0)
                        continue;
                    for (var i = 0; i < 4; i++)
                    {
                        var dirIndex = (directionIndex + i) % 4;
                        var dirName = directionsToMoveIfNoElveNearby[dirIndex].Dir;
                        var dirNeighbourgs = directionsToMoveIfNoElveNearby[dirIndex].Near;
                        if (!dirNeighbourgs
                            .Select(s => EightDirections[s])
                            .Select(x => (elve.X + x.dx, elve.Y + x.dy))
                            .Where(x => elvesPosition.Contains(x))
                            .Any()
                            )
                        {
                            var (dx, dy) = EightDirections[dirName];
                            elve = (elve.X + dx, elve.Y + dy);
                            break;
                        }
                    }
                    if (Elves[id] != elve)
                    {
                        if (elvesCollisions.ContainsKey(elve))
                            elvesCollisions[elve] = id;
                        else
                        {
                            Elves[id] = elve;
                            elvesCollisions.Add(elve, id);
                        }
                    }
                }
                for (var id = 0; id < Elves.Length; id++)
                {
                    var elve = Elves[id];
                    if (elvesCollisions.TryGetValue(elve,out var cid))
                        if ( cid != id )
                            Elves[id] = ElvesPrevPosition[id];
                }
                directionIndex++;
                yield return $"Round {round}";
            }
            var (x1, y1, x2, y2) = (
                Elves.Select(e => e.X).Min(),
                Elves.Select(e => e.Y).Min(),
                Elves.Select(e => e.X).Max(),
                Elves.Select(e => e.Y).Max()
                );
            ElvesPrevPosition = Elves;
            var score = (x2 - x1 + 1) * (y2 - y1 + 1) - Elves.Length;
            yield return $"{score}";
        }
        public IEnumerable<string> SolveSecondPart()
        {
            var round = 0;
            var directionIndex = 0;
            var someElevesMoved = true;
            while (someElevesMoved)
            {
                round++;
                var elvesPosition = Elves.ToHashSet();
                Array.Copy(Elves, ElvesPrevPosition, Elves.Length);
                var elvesCollisions = new Dictionary<(int X, int Y), int>();
                for (var id = 0; id < Elves.Length; id++)
                {
                    var elve = Elves[id];
                    var neighbourgs = EightDirections.Values
                        .Select(x => (elve.X + x.dx, elve.Y + x.dy))
                        .Where(x => elvesPosition.Contains(x)).ToList();
                    if (neighbourgs.Count == 0)
                        continue;
                    for (var i = 0; i < 4; i++)
                    {
                        var dirIndex = (directionIndex + i) % 4;
                        var dirName = directionsToMoveIfNoElveNearby[dirIndex].Dir;
                        var dirNeighbourgs = directionsToMoveIfNoElveNearby[dirIndex].Near;
                        if (!dirNeighbourgs
                            .Select(s => EightDirections[s])
                            .Select(x => (elve.X + x.dx, elve.Y + x.dy))
                            .Where(x => elvesPosition.Contains(x))
                            .Any()
                            )
                        {
                            var (dx, dy) = EightDirections[dirName];
                            elve = (elve.X + dx, elve.Y + dy);
                            break;
                        }
                    }
                    if (Elves[id] != elve)
                    {
                        if (elvesCollisions.ContainsKey(elve))
                            elvesCollisions[elve] = id;
                        else
                        {
                            Elves[id] = elve;
                            elvesCollisions.Add(elve, id);
                        }
                    }
                }
                for (var id = 0; id < Elves.Length; id++)
                {
                    var elve = Elves[id];
                    if (elvesCollisions.TryGetValue(elve, out var cid))
                        if (cid != id)
                            Elves[id] = ElvesPrevPosition[id];
                }
                directionIndex++;
                yield return $"Round {round}";
                someElevesMoved = !Enumerable.SequenceEqual(ElvesPrevPosition, Elves);
            }
            ElvesPrevPosition = Elves;
            yield return $"{round}";
        }
    }
}