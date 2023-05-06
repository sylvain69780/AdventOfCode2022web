namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(23, "Unstable Diffusion")]
    public class UnstableDiffusion : IPuzzleSolver
    {
        public IEnumerable<string> SolveFirstPart(string inp)
        {
            var input = inp.Split("\n");
            var row = 0;
            var elves = input
                .Select(x => (line: x, row: row++))
                .SelectMany(x => Enumerable.Range(0, x.line.Length).Where(col => x.line[col] == '#')
                .Select(col => (x: col, y: x.row)))
                .ToHashSet();

            var directions = new Dictionary<string, (int dx, int dy)>()
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
            var lookAt = new Queue<string>();
            lookAt.Enqueue("N");
            lookAt.Enqueue("S");
            lookAt.Enqueue("W");
            lookAt.Enqueue("E");

            var lookAtDir = new Dictionary<string, List<string>>()
    {
        { "N", new List<string> {"N","NE","NW"} },
        { "S", new List<string> {"S","SE","SW"} },
        { "W", new List<string> {"W","NW","SW"} },
        { "E", new List<string> {"E","NE","SE"} }
    };

            var rounds = 10;

            for (var round = 1; round <= rounds; round++)
            {
                var elvesMoves = new Dictionary<(int x, int y), (int nextX, int nextY)>();
                foreach (var elve in elves)
                {
                    var neighbourgs = directions.Values
                        .Select(x => (elve.x + x.dx, elve.y + x.dy))
                        .Where(x => elves.Contains(x)).Count();
                    if (neighbourgs == 0)
                    {
                        elvesMoves.Add(elve, elve);
                        continue;
                    }
                    foreach (var look in lookAt)
                    {
                        if (
                            !lookAtDir[look]
                            .Select(x => directions[x])
                            .Select(x => (elve.x + x.dx, elve.y + x.dy))
                            .Where(x => elves.Contains(x))
                            .Any()
                            )
                        {
                            var move = directions[look];
                            elvesMoves.Add(elve, (elve.x + move.dx, elve.y + move.dy));
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
                elves = elvesMoves.Values.ToHashSet();
                var v = lookAt.Dequeue();
                lookAt.Enqueue(v);
            }
            var (x1, y1, x2, y2) = (
                elves.Select(x => x.x).Min(),
                elves.Select(x => x.y).Min(),
                elves.Select(x => x.x).Max(),
                elves.Select(x => x.y).Max()
                );
            var score = (x2 - x1 + 1) * (y2 - y1 + 1) - elves.Count;
            yield return $"SCORE = {score}";
        }
        public IEnumerable<string> SolveSecondPart(string inp)
        {
            var input = inp.Split("\n");
            var row = 0;
            var elves = input
                .Select(x => (line: x, row: row++))
                .SelectMany(x => Enumerable.Range(0, x.line.Length).Where(col => x.line[col] == '#')
                .Select(col => (x: col, y: x.row)))
                .ToHashSet();

            var directions = new Dictionary<string, (int dx, int dy)>()
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
            var lookAt = new Queue<string>();
            lookAt.Enqueue("N");
            lookAt.Enqueue("S");
            lookAt.Enqueue("W");
            lookAt.Enqueue("E");

            var lookAtDir = new Dictionary<string, List<string>>()
    {
        { "N", new List<string> {"N","NE","NW"} },
        { "S", new List<string> {"S","SE","SW"} },
        { "W", new List<string> {"W","NW","SW"} },
        { "E", new List<string> {"E","NE","SE"} }
    };

            var noMove = false;
            var round = 0;
            while (!noMove)
            {
                round++;
                var elvesMoves = new Dictionary<(int x, int y), (int nextX, int nextY)>();
                foreach (var elve in elves)
                {
                    var neighbourgs = directions.Values
                        .Select(x => (elve.x + x.dx, elve.y + x.dy))
                        .Where(x => elves.Contains(x)).Count();
                    if (neighbourgs == 0)
                    {
                        elvesMoves.Add(elve, elve);
                        continue;
                    }
                    foreach (var look in lookAt)
                    {
                        if (
                            !lookAtDir[look]
                            .Select(x => directions[x])
                            .Select(x => (elve.x + x.dx, elve.y + x.dy))
                            .Where(x => elves.Contains(x))
                            .Any()
                            )
                        {
                            var move = directions[look];
                            elvesMoves.Add(elve, (elve.x + move.dx, elve.y + move.dy));
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
                var v = lookAt.Dequeue();
                lookAt.Enqueue(v);
                var newElves = elvesMoves.Values.ToHashSet();
                if (noMove = newElves.SetEquals(elves))
                    break;
                elves = newElves;
            }
            var (x1, y1, x2, y2) = (
                elves.Select(x => x.x).Min(),
                elves.Select(x => x.y).Min(),
                elves.Select(x => x.x).Max(),
                elves.Select(x => x.y).Max()
                );
            var score = (x2 - x1 + 1) * (y2 - y1 + 1) - elves.Count;
            yield return $"SCORE = {round}";
        }
    }
}