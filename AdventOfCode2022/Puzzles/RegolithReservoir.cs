﻿using System.Diagnostics;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(14, "Regolith Reservoir")]
    public class RegolithReservoir : IPuzzleSolverV2
    {
        public async Task<string> SolveFirstPart(string puzzleInput, Func<Func<string>,bool, Task> update, CancellationToken cancellationToken)
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
                            map.SetOccupiedInitial((x, beginRock.y));
                    if (beginRock.x == endRock.x)
                        for (var y = Math.Min(beginRock.y, endRock.y); y <= Math.Max(beginRock.y, endRock.y); y++)
                            map.SetOccupiedInitial((beginRock.x,y));
                }
            }

            var iterations = 0;
            while (true)
            {
                map.SandPosition = (500,0);
                var isFreeToMove = true;
                while (isFreeToMove && map.SandPosition.y < floorPosition)
                {
                        await update(() => Visualize(map),false);
                        if (cancellationToken.IsCancellationRequested)
                            return "";
                    var newSandPosition = map.SandPosition;
                    foreach (var (dx, dy) in Directions)
                    {
                        var (x, y) = (map.SandPosition.x + dx, map.SandPosition.y + dy);
                        if (!map.OccupiedPositions!.Contains((x, y)))
                        {
                            newSandPosition = (x, y);
                            break;
                        }
                    }
                    if (newSandPosition == map.SandPosition)
                        isFreeToMove = false;
                    else
                        map.SandPosition = newSandPosition;
                }
                if (map.SandPosition.y >= floorPosition) 
                    break;
                map.SetOccupied(map.SandPosition);
                iterations++;
            }
            await update(() => Visualize(map),true);
            return iterations.ToString();
        }
        public async Task<string> SolveSecondPart(string puzzleInput, Func<Func<string>,bool, Task> update, CancellationToken cancellationToken)
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
                            map.SetOccupiedInitial((x, beginRock.y));
                    if (beginRock.x == endRock.x)
                        for (var y = Math.Min(beginRock.y, endRock.y); y <= Math.Max(beginRock.y, endRock.y); y++)
                            map.SetOccupiedInitial((beginRock.x, y));
                }
            }
            var iterations = 0;
            while (true)
            {
                map.SandPosition = (500,0);
                var isFreeToMove = true;
                while (isFreeToMove)
                {
                        await update(() => Visualize(map),false);
                        if (cancellationToken.IsCancellationRequested)
                            return "";
                    var newSandPosition = map.SandPosition;
                    foreach (var (dx, dy) in Directions)
                    {
                        var (x, y) = (map.SandPosition.x + dx, map.SandPosition.y + dy);
                        if (y < floorPosition && !map.OccupiedPositions!.Contains((x, y)))
                        {
                            newSandPosition = (x, y);
                            break;
                        }
                    }
                    if (newSandPosition == map.SandPosition)
                        isFreeToMove = false;
                    else
                        map.SandPosition = newSandPosition;
                }
                iterations++;
                if (map.SandPosition == (500,0))
                    break;
                map.SetOccupied(map.SandPosition);
            }
            await update(() => Visualize(map),true);
            return iterations.ToString();
        }
        private static readonly (int x, int y)[] Directions = new (int x, int y)[] { (0, 1), (-1, 1), (1, 1) };

        private class Map
        {
            public (int x, int y) SandPosition;
            public readonly HashSet<(int x, int y)>? OccupiedPositions;
            public readonly HashSet<(int x, int y)>? InitialPositions;
            public int xMin = 500;
            public int yMin;
            public int xMax = 500;
            public int yMax;
            public Map()
            {
                OccupiedPositions = new HashSet<(int x, int y)>();
                InitialPositions = new HashSet<(int x, int y)>();
            }
            public void SetOccupiedInitial((int x, int y) position)
            {
                OccupiedPositions!.Add(position);
                InitialPositions!.Add(position);
                xMin = Math.Min(xMin, position.x);
                yMin = Math.Min(yMin, position.y);
                xMax = Math.Max(xMax, position.x);
                yMax = Math.Max(yMax, position.y);
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
            var Width = map.xMax - map.xMin;
            var Height = map.yMax - map.yMin;
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
                                if (map.InitialPositions!.Contains((map.xMin + x, map.yMin + y)))
                                    pixel = Color.Blue;
                                else if (map.OccupiedPositions!.Contains((map.xMin + x, map.yMin + y)))
                                    pixel = Color.White;
                                else if (map.SandPosition == (map.xMin + x, map.yMin + y))
                                    pixel = Color.Red;
                            }
                        }
                    });
                    img.SaveAsPng(outStream);
                }

                response = "data:image/png;base64, " + Convert.ToBase64String(outStream.ToArray());
            }
            return response;
        }
    }
}