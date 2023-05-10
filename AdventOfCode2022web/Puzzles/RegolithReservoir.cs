using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(14, "Regolith Reservoir")]
    public class RegolithReservoir : IPuzzleSolverV2
    {
        private static readonly (int x, int y)[] Directions = new (int x, int y)[] { (0, 1), (-1, 1), (1, 1) };

        public static string Visualize(int iterations)
        {
            return iterations.ToString();
        }

        public async Task<string> SolveFirstPart(string puzzleInput, Func<string, Task> update, CancellationToken cancellationToken)
        {
            var paths = puzzleInput.Split("\n").Select(x => x.Replace(" -> ", "#").Split('#')
                .Select(y => y.Split(','))
                .Select(y => (x: int.Parse(y[0]), y: int.Parse(y[1]))).ToList())
                .ToList();
            var floorPosition = paths.SelectMany(x => x).Select(x => x.y).Max() + 2;
            var occupiedPositions = new HashSet<(int x, int y)>();
            foreach (var rocks in paths)
            {
                for (var i = 0; i < rocks.Count - 1; i++)
                {
                    var beginRock = rocks[i];
                    var endRock = rocks[i + 1];
                    if (beginRock.y == endRock.y)
                        for (var x = Math.Min(beginRock.x, endRock.x); x <= Math.Max(beginRock.x, endRock.x); x++)
                            occupiedPositions.Add((x, beginRock.y));
                    if (beginRock.x == endRock.x)
                        for (var y = Math.Min(beginRock.y, endRock.y); y <= Math.Max(beginRock.y, endRock.y); y++)
                            occupiedPositions.Add((beginRock.x,y));
                }
            }

            var iterations = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (true)
            {
                var sandPosition = (x: 500, y: 0);
                var isFreeToMove = true;
                while (isFreeToMove && sandPosition.y < floorPosition)
                {
                    var newSandPosition = sandPosition;
                    foreach (var (dx, dy) in Directions)
                    {
                        var (x, y) = (sandPosition.x + dx, sandPosition.y + dy);
                        if (!occupiedPositions.Contains((x, y)))
                        {
                            newSandPosition = (x, y);
                            break;
                        }
                    }
                    if (newSandPosition == sandPosition)
                        isFreeToMove = false;
                    else
                        sandPosition = newSandPosition;
                }
                if (sandPosition.y >= floorPosition) 
                    break;
                occupiedPositions.Add(sandPosition);
                iterations++;
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    stopwatch.Restart();
                    await update(Visualize(iterations));
                    if (cancellationToken.IsCancellationRequested)
                        break;
                }
            }
            return iterations.ToString();
        }
        public async Task<string> SolveSecondPart(string puzzleInput, Func<string, Task> update, CancellationToken cancellationToken)
        {
            var paths = puzzleInput.Split("\n").Select(x => x.Replace(" -> ", "#").Split('#')
                .Select(y => y.Split(','))
                .Select(y => (x: int.Parse(y[0]), y: int.Parse(y[1]))).ToList())
                .ToList();
            var floorPosition = paths.SelectMany(x => x).Select(x => x.y).Max() + 2;
            var occupiedPositions = new HashSet<(int x, int y)>();
            foreach (var rocks in paths)
            {
                for (var i = 0; i < rocks.Count - 1; i++)
                {
                    var beginRock = rocks[i];
                    var endRock = rocks[i + 1];
                    if (beginRock.y == endRock.y)
                        for (var x = Math.Min(beginRock.x, endRock.x); x <= Math.Max(beginRock.x, endRock.x); x++)
                            occupiedPositions.Add((x, beginRock.y));
                    if (beginRock.x == endRock.x)
                        for (var y = Math.Min(beginRock.y, endRock.y); y <= Math.Max(beginRock.y, endRock.y); y++)
                            occupiedPositions.Add((beginRock.x, y));
                }
            }
            var iterations = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (true)
            {
                var sandPosition = (x: 500, y: 0);
                var isFreeToMove = true;
                while (isFreeToMove)
                {
                    var newSandPosition = sandPosition;
                    foreach (var (dx, dy) in Directions)
                    {
                        var (x, y) = (sandPosition.x + dx, sandPosition.y + dy);
                        if (y < floorPosition && !occupiedPositions.Contains((x, y)))
                        {
                            newSandPosition = (x, y);
                            break;
                        }
                    }
                    if (newSandPosition == sandPosition)
                        isFreeToMove = false;
                    else
                        sandPosition = newSandPosition;
                }
                iterations++;
                if (sandPosition == (500,0))
                    break;
                occupiedPositions.Add(sandPosition);
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    stopwatch.Restart();
                    await update(Visualize(iterations));
                    if (cancellationToken.IsCancellationRequested)
                        break;
                }
            }
            stopwatch.Stop();
            return iterations.ToString();
        }
    }
}