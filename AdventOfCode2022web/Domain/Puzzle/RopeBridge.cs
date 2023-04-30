namespace AdventOfCode2022web.Domain.Puzzle
{
    public class RopeBridge : PuzzleSolver
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

        protected override string SolveFirst(string puzzleInput)
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
                var (dx, dy) = (head.x - tail.x, head.y - tail.y);
                if (Math.Abs(dx) < 2 && Math.Abs(dy) < 2) continue;
                if (head.x - tail.x > 0) tail.x++;
                if (tail.x - head.x > 0) tail.x--;
                if (head.y - tail.y > 0) tail.y++;
                if (tail.y - head.y > 0) tail.y--;
                visitedPositions.Add(tail);
            }
            return Format(visitedPositions.Count);
        }
        protected override string SolveSecond(string puzzleInput)
        {
            var seriesOfMotions = ToLines(puzzleInput)
                .Select(x => x.Split(" "))
                .SelectMany(x => Enumerable.Range(0, int.Parse(x[1])), (x, y) => x[0]);

            var visited = new HashSet<(int x, int y)>();
            var head = (x: 0, y: 0);
            var tail = new (int x,int y)[9];
            visited.Add((0, 0));
            foreach (var move in seriesOfMotions)
            {
                var (x, y) = Directions[move];
                head.x += x;
                head.y += y;
                var prev = head;
                foreach (var i in Enumerable.Range(0, 9))
                {
                    var (dx, dy) = (prev.x - tail[i].x, prev.y - tail[i].y);
                    if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1)
                    {
                        if (prev.x - tail[i].x > 0) tail[i].x++;
                        if (tail[i].x - prev.x > 0) tail[i].x--;
                        if (prev.y - tail[i].y > 0) tail[i].y++;
                        if (tail[i].y - prev.y > 0) tail[i].y--;
                    }
                    prev = tail[i];
                }
                visited.Add(tail[8]);
            }
            return Format(visited.Count);
        }
    }
}