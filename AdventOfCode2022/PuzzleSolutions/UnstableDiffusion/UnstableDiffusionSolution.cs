using System.Text;
using AdventOfCode2022Solutions.PuzzleSolutions;

namespace AdventOfCode2022Solutions.PuzzleSolutions.UnstableDiffusion
{
    public class UnstableDiffusionSolution : IPuzzleSolution
    {
        private string[]? Input { get; set; }
        public (int X, int Y)[] Elves = Array.Empty<(int X, int Y)>();
        public (int X, int Y)[] ElvesPrevPosition = Array.Empty<(int X, int Y)>();

        private static readonly Dictionary<string, (int dx, int dy)> DirectionsToLookAround = new()
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

        private static readonly (string Dir, List<string> Near)[] directionsToMoveAndProximityCheck = new (string Dir, List<string> Near)[]
        {
            ("N", new List<string> {"N","NE","NW"} ),
            ( "S", new List<string> {"S","SE","SW"} ),
            ( "W", new List<string> {"W","NW","SW"} ),
            ( "E", new List<string> {"E","NE","SE"} )
        };

        public void Initialize(string puzzleInput)
        {
            Input = puzzleInput.Split("\n");
            Reset();
        }

        public void Reset()
        {
            var row = 0;
            Elves = Input!
                .Select(x => (line: x, row: row++))
                .SelectMany(x => Enumerable.Range(0, x.line.Length).Where(col => x.line[col] == '#')
                .Select(col => (col, x.row)))
                .ToArray();
            ElvesPrevPosition = Elves.ToArray();
        }

        public IEnumerable<string> SolveFirstPart()
        {
            Reset();
            var rounds = 10;
            var directionIndex = 0;
            for (var round = 1; round <= rounds; round++)
            {
                SimulateDiffusion(directionIndex);
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
            Reset();
            var round = 0;
            var directionIndex = 0;
            var someElevesMoved = true;
            while (someElevesMoved)
            {
                round++;
                SimulateDiffusion(directionIndex);
                directionIndex++;
                yield return $"Round {round}";
                someElevesMoved = !ElvesPrevPosition.SequenceEqual(Elves);
            }
            ElvesPrevPosition = Elves;
            yield return $"{round}";
        }

        private void SimulateDiffusion(int directionIndex)
        {
            var elvesPosition = Elves.ToHashSet();
            Array.Copy(Elves, ElvesPrevPosition, Elves.Length);
            var elvesCollisions = new Dictionary<(int X, int Y), int>();
            for (var id = 0; id < Elves.Length; id++)
            {
                var elve = Elves[id];
                var neighbours = DirectionsToLookAround.Values
                    .Select(x => (elve.X + x.dx, elve.Y + x.dy))
                    .Where(x => elvesPosition.Contains(x)).ToList();
                if (neighbours.Count == 0)
                    continue;
                for (var i = 0; i < 4; i++)
                {
                    var dirIndex = (directionIndex + i) % 4;
                    var dirName = directionsToMoveAndProximityCheck[dirIndex].Dir;
                    var dirNeighbourgs = directionsToMoveAndProximityCheck[dirIndex].Near;
                    if (!dirNeighbourgs
                        .Select(s => DirectionsToLookAround[s])
                        .Select(x => (elve.X + x.dx, elve.Y + x.dy))
                        .Where(x => elvesPosition.Contains(x))
                        .Any()
                        )
                    {
                        var (dx, dy) = DirectionsToLookAround[dirName];
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
        }
    }
}