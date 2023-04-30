namespace AdventOfCode2022web.Domain.Puzzle
{
    public class RopeBridge : IPuzzleSolver
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

        private static (int x,int y) MoveTailPosition((int x, int y) tail, (int x, int y) head)
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

        public IEnumerable<string> SolveFirstPart(string puzzleInput)
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
            yield return Format(visitedPositions.Count);
        }
        public IEnumerable<string> SolveSecondPart(string puzzleInput)
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
                var previous = head;
                foreach (var i in Enumerable.Range(0, 9))
                {
                    tail[i] = MoveTailPosition(tail[i], previous);
                    previous = tail[i];
                }
                visited.Add(tail[8]);
            }
            yield return Format(visited.Count);
        }
    }
}