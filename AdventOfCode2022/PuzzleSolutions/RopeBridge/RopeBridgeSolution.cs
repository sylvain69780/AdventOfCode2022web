using System.Text;
using AdventOfCode2022Solutions.PuzzleSolutions;

namespace AdventOfCode2022Solutions.PuzzleSolutions.RopeBridge
{
    public class RopeBridgeSolution : IPuzzleSolutionIter
    {
        private string _puzzleInput = string.Empty;

        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }

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

        public IEnumerable<string> SolveFirstPart()
        {
            var seriesOfMotions = ToLines(_puzzleInput)
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
            yield return Format(visitedPositions.Count);
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

        public IEnumerable<string> SolveSecondPart()
        {
            var seriesOfMotions = ToLines(_puzzleInput)
                .Select(x => x.Split(" "))
                .SelectMany(x => Enumerable.Range(0, int.Parse(x[1])), (x, y) => x[0]);

            var visited = new HashSet<(int x, int y)>();
            var head = (x: 0, y: 0);
            var tails = new (int x, int y)[9];
            visited.Add((0, 0));

            var visualizer = new Visualizer();
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
                visited.Add(tails[8]);
            }
            yield return visited.Count.ToString();
        }
    }
}