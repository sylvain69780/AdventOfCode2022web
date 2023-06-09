﻿using System.Text;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(9, "Rope Bridge")]
    public class RopeBridge : IPuzzleSolverV2
    {
        private static string[] ToLines(string s) => s.Split("\n");
        private static string Format(int v) => v.ToString();

        private static readonly Dictionary<string, (int x, int y)> Directions = new()
                {
                    { "R", (1,0) },
                    { "L", (-1,0)},
                    { "U", (0,1) },
                    { "D", (0,-1)},
                };

        private static (int x, int y) MoveTailPosition((int x, int y) tail, (int x, int y) head)
        {
            var newTail = tail;
            var (dx, dy) = (head.x - newTail.x, head.y - newTail.y);
            if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1)
            {
                if (head.x - newTail.x > 0) newTail.x++;
                if (newTail.x - head.x > 0) newTail.x--;
                if (head.y - newTail.y > 0) newTail.y++;
                if (newTail.y - head.y > 0) newTail.y--;
            }
            return newTail;
        }

        public async Task<string> SolveFirstPart(string puzzleInput, Func<Func<string>,bool, Task> func, CancellationToken cancellationToken)
        {
            var seriesOfMotions = ToLines(puzzleInput)
                .Select(x => x.Split(" "))
                .SelectMany(x => Enumerable.Range(0, int.Parse(x[1])), (x, y) => x[0]);
            var visitedPositions = new HashSet<(int x, int y)>();
            var head = (x: 0, y: 0);
            var tail = (x: 0, y: 0);
            visitedPositions.Add(tail);
            foreach (var move in seriesOfMotions)
            {
                var (x, y) = Directions[move];
                head.x += x;
                head.y += y;
                tail = MoveTailPosition(tail, head);
                visitedPositions.Add(tail);
            }
            await func(() => "No visualization available.",true);
            return Format(visitedPositions.Count);
        }

        private class Visualizer
        {
            private int xMin;
            private int yMin;
            private int xMax;
            private int yMax;

            public string Visualize((int x, int y) head, (int x, int y)[] tails, HashSet<(int x, int y)> visited)
            {
                var items = tails.AsEnumerable().Append(head).Concat(visited);
                foreach (var (tx, ty) in items)
                {
                    xMax = Math.Max(xMax, tx);
                    yMax = Math.Max(yMax, ty);
                    xMin = Math.Min(xMin, tx);
                    yMin = Math.Min(yMin, ty);
                }
                var sb = new StringBuilder();
                for (var y = yMin; y <= yMax; y++)
                {
                    for (var x = xMin; x <= xMax; x++)
                    {
                        var c = ' ';
                        if (visited.Contains((x, y))) c = '.';
                        if (tails.Contains((x, y))) c = 'T';
                        if ((x, y) == head) c = 'H';
                        sb.Append(c);
                    }
                    sb.Append('\n');
                }
                return sb.Insert(0, $"The tail visited {visited.Count} positions." + '\n').ToString();
            }
        }

        public async Task<string> SolveSecondPart(string puzzleInput, Func<Func<string>,bool, Task> func, CancellationToken cancellationToken)
        {
            var seriesOfMotions = ToLines(puzzleInput)
                .Select(x => x.Split(" "))
                .SelectMany(x => Enumerable.Range(0, int.Parse(x[1])), (x, y) => x[0]);

            var visited = new HashSet<(int x, int y)>();
            var head = (x: 0, y: 0);
            var tails = new (int x, int y)[9];
            visited.Add((0, 0));

            var visualizer = new Visualizer();
 //           var count = 0;
            foreach (var move in seriesOfMotions)
            {
                var (x, y) = Directions[move];
                head.x += x;
                head.y += y;
                var previous = head;
                foreach (var i in Enumerable.Range(0, 9))
                {
                    tails[i] = MoveTailPosition(tails[i], previous);
                    previous = tails[i];
                }
//                if (count++ < 200)
                    await func(() => visualizer.Visualize(head, tails, visited),false);
                    if (cancellationToken.IsCancellationRequested)
                        return "Cancelled";
                visited.Add(tails[8]);
            }
            await func(() => visualizer.Visualize(head, tails, visited), true);
            return visited.Count.ToString();
        }
    }
}