using System.Text.RegularExpressions;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(15, "Beacon Exclusion Zone")]
    public class BeaconExclusionZone : IPuzzleSolver
    {
        struct Pt
        {
            public int x;
            public int y;
        }

        public string SolveFirstPart(string inp)
        {
            var r = new Regex(@"x=(-?\d+), y=(-?\d+)");
            var input = inp.Split("\n")
                .Select(x => r.Matches(x))
                .Select(x => new
                {
                    Sensor = new Pt
                    {
                        x = int.Parse(x[0].Groups[1].Value),
                        y = int.Parse(x[0].Groups[2].Value)
                    },
                    Beacon = new Pt
                    {
                        x = int.Parse(x[1].Groups[1].Value),
                        y = int.Parse(x[1].Groups[2].Value)
                    }
                })
                .ToList();
            var covered = new HashSet<(int, int)>();
            var discard = input.Select(x => (x.Beacon.x, x.Beacon.y)).ToHashSet();
            discard.UnionWith(input.Select(x => (x.Sensor.x, x.Sensor.y)).ToHashSet());
            var h = input.Count <= 14 ? 10 : 2000000; // looking at this row
            var inters = new List<(int xmin, int xmax)>();
            foreach (var x in input)
            {
                var dist = Math.Abs(x.Sensor.x - x.Beacon.x) + Math.Abs(x.Sensor.y - x.Beacon.y);
                var dh = Math.Abs(x.Sensor.y - h);
                if (dh > dist) continue;
                dist -= dh;
                var inter = (xmin: x.Sensor.x - dist, xmax: x.Sensor.x + dist);
                inters.Add(inter);
            }
            var start = inters.Select(x => x.xmin).Min(); //  not tested
            var end = inters.Select(x => x.xmax).Max();
            var score = 0;
            for (var i = start; i <= end; i++)
            {
                var p = (i, h);
                if (discard.Contains(p)) continue;
                foreach (var inter in inters)
                {
                    if (inter.Item1 <= i && inter.Item2 >= i)
                    {
                        score++;
                        break;
                    }
                }
            }
            return score.ToString();
        }
        public string SolveSecondPart(string inp)
        {
            var r = new Regex(@"x=(-?\d+), y=(-?\d+)");
            var input = inp.Split("\n")
                .Select(x => r.Matches(x))
                .Select(x => new
                {
                    Sensor = new Pt
                    {
                        x = int.Parse(x[0].Groups[1].Value),
                        y = int.Parse(x[0].Groups[2].Value)
                    },
                    Beacon = new Pt
                    {
                        x = int.Parse(x[1].Groups[1].Value),
                        y = int.Parse(x[1].Groups[2].Value)
                    }
                })
                .ToList();
            var discard = input.Select(x => (x.Beacon.x, x.Beacon.y)).ToHashSet();
            discard.UnionWith(input.Select(x => (x.Sensor.x, x.Sensor.y)).ToHashSet());
            var q = new Queue<(int, int, int, int)>();
            var cmax = 4000000;
            var cnt = (int)Math.Sqrt(cmax) + 3;
            q.Enqueue((0, 0, cmax, cmax));
            do
            {
                var nq = new Queue<(int, int, int, int)>();
                while (q.Count > 0)
                {
                    var square = q.Dequeue();
                    var (ax, ay, bx, by) = square;
                    var ps = new (int, int)[] { (ax, ay), (ax, by), (bx, by), (bx, ay) };
                    // intersection test
                    bool isCandidate = true;
                    foreach (var data in input)
                    {
                        var dist = Math.Abs(data.Sensor.x - data.Beacon.x) + Math.Abs(data.Sensor.y - data.Beacon.y);
                        isCandidate = ps.Any(y => Math.Abs(data.Sensor.x - y.Item1) + Math.Abs(data.Sensor.y - y.Item2) > dist);
                        if (!isCandidate) break;
                    }
                    if (isCandidate)
                    {
                        var (dx, dy) = ((bx - ax + 1) / 2, (by - ay + 1) / 2);
                        if (dx > 0 || dy > 0)
                        {
                            nq.Enqueue((ax, ay, bx - dx, by - dy));
                            nq.Enqueue((ax + dx, ay, bx, by - dy));
                            nq.Enqueue((ax, ay + dy, bx - dx, by));
                            nq.Enqueue((ax + dx, ay + dy, bx, by));
                        }
                        else
                            if (!discard.Contains((ax, ay))) nq.Enqueue((ax, ay, ax, ay));
                    }
                }
                q = nq;
                // yield return "Queue lenght = " + q.Count.ToString();
            } while (q.Count > 1 && cnt-- != 0);
            var res = q.Dequeue();
            // too big for int
            return ((long)res.Item1 * 4000000 + res.Item2).ToString();
        }
    }
}