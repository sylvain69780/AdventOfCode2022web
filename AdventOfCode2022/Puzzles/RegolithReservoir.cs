using System.Diagnostics;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(14, "Regolith Reservoir")]
    public class RegolithReservoir : IPuzzleSolverV2
    {
        private static readonly (int x, int y)[] Directions = new (int x, int y)[] { (0, 1), (-1, 1), (1, 1) };

        private class Map
        {
            public readonly HashSet<(int x, int y)>? OccupiedPositions;
            public int xMin = 500;
            public int yMin;
            public int xMax = 500;
            public int yMax;
            public Map()
            {
                OccupiedPositions = new HashSet<(int x, int y)>();
            }
            public void SetOccupied((int x, int y) position)
            {
                OccupiedPositions!.Add(position);
                xMin = Math.Min(xMin, position.x);
                yMin = Math.Min(yMin, position.y);
                xMax = Math.Max(xMax, position.x);
                yMax = Math.Max(yMax, position.y);
            }
        }

        private static string Visualize(Map map)
        {
            var Width = map.xMax-map.xMin;
            var Height = map.yMax-map.yMin;
            var response = string.Empty;
            using (MemoryStream outStream = new())
            {
                using (Image<Rgba32> img = new(Width, Height))
                {
                    img.ProcessPixelRows(accessor =>
                    {
                        for (int y = 0; y < accessor.Height; y++)
                        {
                            Span<Rgba32> pixelRow = accessor.GetRowSpan(y);
                            for (int x = 0; x < pixelRow.Length; x++)
                            {
                                ref Rgba32 pixel = ref pixelRow[x];
                                if (map.OccupiedPositions!.Contains((map.xMin + x, map.yMin + y)))
                                    pixel = Color.White;
                            }
                        }
                    });
                    img.SaveAsPng(outStream);
                }

                response = "data:image/png;base64, " + Convert.ToBase64String(outStream.ToArray());
            }
            return response;
        }

        public async Task<string> SolveFirstPart(string puzzleInput, Func<string, Task> update, CancellationToken cancellationToken)
        {
            var paths = puzzleInput.Split("\n").Select(x => x.Replace(" -> ", "#").Split('#')
                .Select(y => y.Split(','))
                .Select(y => (x: int.Parse(y[0]), y: int.Parse(y[1]))).ToList())
                .ToList();
            var floorPosition = paths.SelectMany(x => x).Select(x => x.y).Max() + 2;
            var map = new Map();
            foreach (var rocks in paths)
            {
                for (var i = 0; i < rocks.Count - 1; i++)
                {
                    var beginRock = rocks[i];
                    var endRock = rocks[i + 1];
                    if (beginRock.y == endRock.y)
                        for (var x = Math.Min(beginRock.x, endRock.x); x <= Math.Max(beginRock.x, endRock.x); x++)
                            map.SetOccupied((x, beginRock.y));
                    if (beginRock.x == endRock.x)
                        for (var y = Math.Min(beginRock.y, endRock.y); y <= Math.Max(beginRock.y, endRock.y); y++)
                            map.SetOccupied((beginRock.x,y));
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
                        if (!map.OccupiedPositions!.Contains((x, y)))
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
                map.SetOccupied(sandPosition);
                iterations++;
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    stopwatch.Restart();
                    await update(Visualize(map));
                    if (cancellationToken.IsCancellationRequested)
                        break;
                }
            }
            await update(Visualize(map));
            return iterations.ToString();
        }
        public async Task<string> SolveSecondPart(string puzzleInput, Func<string, Task> update, CancellationToken cancellationToken)
        {
            var paths = puzzleInput.Split("\n").Select(x => x.Replace(" -> ", "#").Split('#')
                .Select(y => y.Split(','))
                .Select(y => (x: int.Parse(y[0]), y: int.Parse(y[1]))).ToList())
                .ToList();
            var floorPosition = paths.SelectMany(x => x).Select(x => x.y).Max() + 2;
            var map = new Map();
            foreach (var rocks in paths)
            {
                for (var i = 0; i < rocks.Count - 1; i++)
                {
                    var beginRock = rocks[i];
                    var endRock = rocks[i + 1];
                    if (beginRock.y == endRock.y)
                        for (var x = Math.Min(beginRock.x, endRock.x); x <= Math.Max(beginRock.x, endRock.x); x++)
                            map.SetOccupied((x, beginRock.y));
                    if (beginRock.x == endRock.x)
                        for (var y = Math.Min(beginRock.y, endRock.y); y <= Math.Max(beginRock.y, endRock.y); y++)
                            map.SetOccupied((beginRock.x, y));
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
                        if (y < floorPosition && !map.OccupiedPositions!.Contains((x, y)))
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
                map.SetOccupied(sandPosition);
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    stopwatch.Restart();
                    await update(Visualize(map));
                    if (cancellationToken.IsCancellationRequested)
                        break;
                }
            }
            await update(Visualize(map));
            stopwatch.Stop();
            return iterations.ToString();
        }
    }
}