namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(14, "Regolith Reservoir")]
    public class RegolithReservoir : IPuzzleSolver
    {
        struct Pt
        {
            public int x;
            public int y;
        }
        public string SolveFirstPart(string inp)
        {
            var input = inp.Split("\n").Select(x => x.Replace(" -> ", "#").Split('#')
                .Select(y => y.Split(','))
                .Select(y => new Pt { x = int.Parse(y[0]), y = int.Parse(y[1]) }).ToList())
                .ToList()
                ;
            var floor = input.SelectMany(x => x).Select(x => x.y).Max() + 2;
            var start = new Pt { x = 500, y = 0 };
            var directions = new (int, int)[] { (0, 1), (-1, 1), (1, 1), (0, 0) };
            var rest = new HashSet<(int, int)>();
            var sand = start;
            var score = 0;
            do
            {
                score++;
                sand = start;
                var moving = true;
                while (moving && sand.y < floor)
                {
                    var nsand = sand;
                    foreach (var (dx, dy) in directions)
                    {
                        nsand = new Pt { x = sand.x + dx, y = sand.y + dy };
                        if (dx == 0 && dy == 0) break;
                        if (rest.Contains((nsand.x, nsand.y))) continue;
                        var isRockBlocking = false;
                        foreach (var rocks in input)
                        {
                            foreach (var i in Enumerable.Range(0, rocks.Count - 1))
                            {
                                var begin = rocks[i];
                                var end = rocks[i + 1];
                                if (begin.y == end.y)
                                    isRockBlocking = nsand.y == begin.y && (begin.x <= nsand.x && nsand.x <= end.x || end.x <= nsand.x && nsand.x <= begin.x);
                                else if (begin.x == end.x)
                                    isRockBlocking = nsand.x == begin.x && (begin.y <= nsand.y && nsand.y <= end.y || end.y <= nsand.y && nsand.y <= begin.y);
                                if (isRockBlocking) break;
                            }
                            if (isRockBlocking) break;
                        }
                        if (!isRockBlocking) break;
                    }
                    moving = nsand.x != sand.x || nsand.y != sand.y;
                    if (moving)
                        sand = nsand;
                    else
                        rest.Add((sand.x, sand.y));
                    // Console.WriteLine($"{sand.x} {sand.y}");
                }
                Console.WriteLine(score);
                ///yield return score.ToString();

            } while (sand.y < floor);
            score--;
            return score.ToString();

        }
        public string SolveSecondPart(string inp)
        {
            var input = inp.Split("\n").Select(x => x.Replace(" -> ", "#").Split('#')
        .Select(y => y.Split(','))
        .Select(y => new Pt { x = int.Parse(y[0]), y = int.Parse(y[1]) }).ToList())
        .ToList()
        ;
            var floor = input.SelectMany(x => x).Select(x => x.y).Max() + 2;
            input.Add(
                new List<Pt> {
            new Pt { x = int.MinValue, y = floor },
            new Pt { x = int.MaxValue, y = floor }
                });
            var start = new Pt { x = 500, y = 0 };
            var directions = new (int, int)[] { (0, 1), (-1, 1), (1, 1), (0, 0) };
            var rest = new HashSet<(int, int)>();
            var sand = start;
            var score = 0;
            do
            {
                score++;
                sand = start;
                var moving = true;
                while (moving)
                {
                    var nsand = sand;
                    foreach (var (dx, dy) in directions)
                    {
                        nsand = new Pt { x = sand.x + dx, y = sand.y + dy };
                        if (dx == 0 && dy == 0) break;
                        if (rest.Contains((nsand.x, nsand.y))) continue;
                        var isRockBlocking = false;
                        foreach (var rocks in input)
                        {
                            foreach (var i in Enumerable.Range(0, rocks.Count - 1))
                            {
                                var begin = rocks[i];
                                var end = rocks[i + 1];
                                if (begin.y == end.y)
                                    isRockBlocking = nsand.y == begin.y && (begin.x <= nsand.x && nsand.x <= end.x || end.x <= nsand.x && nsand.x <= begin.x);
                                else if (begin.x == end.x)
                                    isRockBlocking = nsand.x == begin.x && (begin.y <= nsand.y && nsand.y <= end.y || end.y <= nsand.y && nsand.y <= begin.y);
                                if (isRockBlocking) break;
                            }
                            if (isRockBlocking) break;
                        }
                        if (!isRockBlocking) break;
                    }
                    moving = nsand.x != sand.x || nsand.y != sand.y;
                    if (moving)
                        sand = nsand;
                    else
                        rest.Add((sand.x, sand.y));
                    // Console.WriteLine($"{sand.x} {sand.y}");
                }
                Console.WriteLine(score);
                //yield return score.ToString();
            } while (sand.y != 0);
            return score.ToString();
        }
    }
}