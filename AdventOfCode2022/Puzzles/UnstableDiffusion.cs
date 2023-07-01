using System.Text;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(23, "Unstable Diffusion")]
    public class UnstableDiffusion : IPuzzleSolverV3
    {
        private HashSet<(int X, int Y)> ElvesPosition;

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

        private static readonly Dictionary<string, List<string>> directionsToMoveIfNoElveNearby = new()
        {
            { "N", new List<string> {"N","NE","NW"} },
            { "S", new List<string> {"S","SE","SW"} },
            { "W", new List<string> {"W","NW","SW"} },
            { "E", new List<string> {"E","NE","SE"} }
        };

        public void Setup(string puzzleInput)
        {
            ElvesPosition = new HashSet<(int X, int Y)>();
            var input = puzzleInput.Split("\n");
            var row = 0;
            ElvesPosition = input
                .Select(x => (line: x, row: row++))
                .SelectMany(x => Enumerable.Range(0, x.line.Length).Where(col => x.line[col] == '#')
                .Select(col => (x: col, y: x.row)))
                .ToHashSet();

        }

        public string Visualize()
        {
            var xmin = ElvesPosition.Min(e => e.X);
            var ymin = ElvesPosition.Min(e => e.Y);
            var xmax = ElvesPosition.Max(e => e.X);
            var ymax = ElvesPosition.Max(e => e.Y);
            var sb = new StringBuilder();
            for ( var y = ymin; y<=ymax; y++)
            {
                for (var x = xmin; x <= xmax; x++) 
                {
                    sb.Append(ElvesPosition.Contains((x, y)) ? '#' : '.');
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }

        public IEnumerable<string> SolveFirstPart()
        {
            var directionPreferedOrder = new Queue<string>();
            directionPreferedOrder.Enqueue("N");
            directionPreferedOrder.Enqueue("S");
            directionPreferedOrder.Enqueue("W");
            directionPreferedOrder.Enqueue("E");
            var rounds = 10;

            for (var round = 1; round <= rounds; round++)
            {
                var elvesMoves = new Dictionary<(int x, int y), (int nextX, int nextY)>();
                foreach (var elve in ElvesPosition)
                {
                    var neighbourgs = EightDirections.Values
                        .Select(x => (elve.X + x.dx, elve.Y + x.dy))
                        .Where(x => ElvesPosition.Contains(x)).ToList();
                    if (neighbourgs.Count == 0)
                    {
                        elvesMoves.Add(elve, elve);
                        continue;
                    }
                    foreach (var dir in directionPreferedOrder)
                    {
                        if (
                            !directionsToMoveIfNoElveNearby[dir]
                            .Select(x => EightDirections[x])
                            .Select(x => (elve.X + x.dx, elve.Y + x.dy))
                            .Where(x => ElvesPosition.Contains(x))
                            .Any()
                            )
                        {
                            var move = EightDirections[dir];
                            elvesMoves.Add(elve, (elve.X + move.dx, elve.Y + move.dy));
                            break;
                        }
                    }
                    if (!elvesMoves.ContainsKey(elve))
                        elvesMoves.Add(elve, elve);
                }
                var elvesBlocked = elvesMoves.Select(x => (x.Key, x.Value)).GroupBy(x => x.Value, x => x.Key)
                    .Where(x => x.Count() > 1).SelectMany(x => x);
                foreach (var elve in elvesBlocked)
                {
                    elvesMoves[elve] = elve;
                }
                ElvesPosition = elvesMoves.Values.ToHashSet();
                var v = directionPreferedOrder.Dequeue();
                directionPreferedOrder.Enqueue(v);
                yield return $"Round {round}";
            }
            var (x1, y1, x2, y2) = (
                ElvesPosition.Select(e => e.X).Min(),
                ElvesPosition.Select(e => e.Y).Min(),
                ElvesPosition.Select(e => e.X).Max(),
                ElvesPosition.Select(e => e.Y).Max()
                );
            var score = (x2 - x1 + 1) * (y2 - y1 + 1) - ElvesPosition.Count;
            yield return $"{score}";
        }
        public IEnumerable<string> SolveSecondPart()
        {

            var directionPreferedOrder = new Queue<string>();
            directionPreferedOrder.Enqueue("N");
            directionPreferedOrder.Enqueue("S");
            directionPreferedOrder.Enqueue("W");
            directionPreferedOrder.Enqueue("E");

            var noMoveDone = false;
            var round = 0;
            while (!noMoveDone)
            {
                round++;
                var elvesMoves = new Dictionary<(int x, int y), (int nextX, int nextY)>();
                foreach (var elve in ElvesPosition)
                {
                    var neighbourgs = EightDirections.Values
                        .Select(x => (elve.X + x.dx, elve.Y + x.dy))
                        .Where(x => ElvesPosition.Contains(x)).Count();
                    if (neighbourgs == 0)
                    {
                        elvesMoves.Add(elve, elve);
                        continue;
                    }
                    foreach (var look in directionPreferedOrder)
                    {
                        if (
                            !directionsToMoveIfNoElveNearby[look]
                            .Select(x => EightDirections[x])
                            .Select(x => (elve.X + x.dx, elve.Y + x.dy))
                            .Where(x => ElvesPosition.Contains(x))
                            .Any()
                            )
                        {
                            var move = EightDirections[look];
                            elvesMoves.Add(elve, (elve.X + move.dx, elve.Y + move.dy));
                            break;
                        }
                    }
                    if (!elvesMoves.ContainsKey(elve))
                        elvesMoves.Add(elve, elve);
                }
                var elvesBlocked = elvesMoves.Select(x => (x.Key, x.Value)).GroupBy(x => x.Value, x => x.Key)
                    .Where(x => x.Count() > 1).SelectMany(x => x);
                foreach (var elve in elvesBlocked)
                {
                    elvesMoves[elve] = elve;
                }
                var v = directionPreferedOrder.Dequeue();
                directionPreferedOrder.Enqueue(v);
                var newElves = elvesMoves.Values.ToHashSet();
                if (noMoveDone = newElves.SetEquals(ElvesPosition))
                    break;
                ElvesPosition = newElves;
                yield return $"Round {round}";
            }
            var (x1, y1, x2, y2) = (
                ElvesPosition.Select(x => x.X).Min(),
                ElvesPosition.Select(x => x.Y).Min(),
                ElvesPosition.Select(x => x.X).Max(),
                ElvesPosition.Select(x => x.Y).Max()
                );
            var score = (x2 - x1 + 1) * (y2 - y1 + 1) - ElvesPosition.Count;
            yield return $"{round}";
        }
    }
}