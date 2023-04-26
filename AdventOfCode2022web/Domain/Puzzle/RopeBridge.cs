namespace AdventOfCode2022web.Domain.Puzzle
{
    public class RopeBridge : IPuzzleSolver
    {
        struct Pt
        {
            public int x;
            public int y;
        }
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            var input = Input.Split("\n")
                .Select(x => x.Split(" "))
                .SelectMany(x => Enumerable.Range(0, int.Parse(x[1])), (x, y) => x[0]);
            var directions = new Dictionary<string, Pt>()
                {
                    { "R", new Pt{x=1,y=0 } },
                    { "L", new Pt{x=-1,y=0 } },
                    { "U", new Pt{x=0,y=1 } },
                    { "D", new Pt { x =0,y=-1 } },
                };
            var visited = new HashSet<(int, int)>();
            var h = (x: 0, y: 0);
            var t = (x: 0, y: 0);
            visited.Add(t);
            foreach (var d in input)
            {
                var dir = directions[d];
                h.x += dir.x;
                h.y += dir.y;
                var (dx, dy) = (h.x - t.x, h.y - t.y);
                if (Math.Abs(dx) < 2 && Math.Abs(dy) < 2) continue;
                if (h.x - t.x > 0) t.x++;
                if (t.x - h.x > 0) t.x--;
                if (h.y - t.y > 0) t.y++;
                if (t.y - h.y > 0) t.y--;
                visited.Add(t);
            }
            var result = visited.Count;
            return result.ToString();
        }
        public string Part2()
        {
            var input = Input.Split("\n")
                .Select(x => x.Split(" "))
                .SelectMany(x => Enumerable.Range(0, int.Parse(x[1])), (x, y) => x[0]);
            var directions = new Dictionary<string, Pt>()
            {
                { "R", new Pt{x=1,y=0 } },
                { "L", new Pt{x=-1,y=0 } },
                { "U", new Pt{x=0,y=1 } },
                { "D", new Pt { x =0,y=-1 } },
            };
            var visited = new HashSet<(int, int)>();
            var h = (x: 0, y: 0);
            var t = new Pt[9];
            visited.Add((0, 0));
            foreach (var d in input)
            {
                var dir = directions[d];
                h.x += dir.x;
                h.y += dir.y;
                var prev = new Pt { x = h.x, y = h.y };
                foreach (var i in Enumerable.Range(0, 9))
                {
                    var (dx, dy) = (prev.x - t[i].x, prev.y - t[i].y);
                    if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1)
                    {
                        if (prev.x - t[i].x > 0) t[i].x++;
                        if (t[i].x - prev.x > 0) t[i].x--;
                        if (prev.y - t[i].y > 0) t[i].y++;
                        if (t[i].y - prev.y > 0) t[i].y--;
                    }
                    prev = t[i];
                }
                visited.Add((t[8].x, t[8].y));
            }
            var result = visited.Count;
            return result.ToString();
        }
    }
}