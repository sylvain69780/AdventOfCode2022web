using System.Text.RegularExpressions;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(16, "Proboscidea Volcanium")]
    public class ProboscideaVolcanium : IPuzzleSolver
    {
        struct PipeVisitor
        {
            public string Position;
            public string PositionElephant;
            public List<string> Journal;
            public List<string> JournalElephant;
            public int Pressure;
            public List<string> Opened;
        }
        public string SolveFirstPart(string inp)
        {
            var r = new Regex(@"Valve (..) has flow rate=(\d+); tunnels? leads? to valves? (.+)");
            var input = inp.Split("\n")
                .Select(x => r.Matches(x))
                .Select(x => new
                {
                    Valve = x[0].Groups[1].Value,
                    Rate = int.Parse(x[0].Groups[2].Value),
                    Valves = x[0].Groups[3].Value.Replace(", ", ",").Split(',')
                })
                .ToList();

            var visitors = new Queue<PipeVisitor>();
            var scores = new List<PipeVisitor>();
            var score = 0;
            visitors.Enqueue(new PipeVisitor
            {
                Position = "AA",
                Journal = new List<string>(),
                Opened = new List<string>(),
                Pressure = 0
            });
            var ignore = input.Where(x => x.Rate == 0).Select(x => x.Valve).ToList();
            var valvesToOpen = input.Count - ignore.Count;
            var i = 30;
            var maxPressure = 0;
            while (visitors.Count > 0 && i-- > 0)
            {
                Console.WriteLine($"Iteration {i} queue lenght {visitors.Count}");
                var newQueue = new Queue<PipeVisitor>();
                while (visitors.Count > 0)
                {
                    var visitor = visitors.Dequeue();
                    var rate = input.Where(x => visitor.Opened.Contains(x.Valve)).Select(x => x.Rate).Sum();
                    visitor.Pressure += rate;
                    if (visitor.Pressure > maxPressure)
                        maxPressure = visitor.Pressure;
                    if (visitor.Pressure < maxPressure / 2)
                        continue;
                    var item = input.Find(x => x.Valve == visitor.Position) ?? throw new ArgumentNullException();
                    // Console.WriteLine($"Visitor at {visitor.Position} pressure {visitor.Pressure} can go at " + string.Join(',', item.Valves) + " already visited " + string.Join(',', visitor.Journal));
                    visitor.Journal.Add(visitor.Position);
                    // choice to open the valve
                    if (!ignore.Contains(visitor.Position) && !visitor.Opened.Contains(visitor.Position))
                    {
                        var newOpened = visitor.Opened.GetRange(0, visitor.Opened.Count);
                        newOpened.Add(visitor.Position);
                        var newVisitor = new PipeVisitor
                        {
                            Position = visitor.Position,
                            Pressure = visitor.Pressure,
                            Journal = new List<string>(),
                            Opened = newOpened
                        };
                        if (newVisitor.Opened.Count == valvesToOpen)
                        {
                            //Console.WriteLine($"Visitor at {newVisitor.Position} pressure {newVisitor.Pressure} no more valves to open.");
                            //Console.WriteLine($"Rate {rate} and {i} {i*rate}");
                            rate = input.Where(x => newVisitor.Opened.Contains(x.Valve)).Select(x => x.Rate).Sum();
                            newVisitor.Pressure += i * rate;
                            if (newVisitor.Pressure >= score)
                            {
                                score = newVisitor.Pressure;
                                Console.WriteLine($"Visitor at {newVisitor.Position} pressure Rate {rate} END {newVisitor.Pressure} opened {string.Join(',', newVisitor.Opened)}.");
                                //return $"Visitor at {newVisitor.Position} pressure Rate {rate} END {newVisitor.Pressure} opened {string.Join(',', newVisitor.Opened)}.";
                                newVisitor.Journal = visitor.Journal.GetRange(0, visitor.Journal.Count);
                                scores.Add(newVisitor);
                            }
                        }
                        else
                            newQueue.Enqueue(newVisitor);
                    }
                    // choice to move to another valve
                    foreach (var child in item.Valves)
                        if (!visitor.Journal.Contains(child))
                            newQueue.Enqueue(new PipeVisitor
                            {
                                Position = child,
                                Pressure = visitor.Pressure,
                                Journal = visitor.Journal.GetRange(0, visitor.Journal.Count),
                                Opened = visitor.Opened.GetRange(0, visitor.Opened.Count)
                            });
                }
                visitors = newQueue;
                if (visitors.Count > 0)
                {
                    var win = visitors.OrderByDescending(x => x.Pressure).First();
                    Console.WriteLine($"{string.Join(',', win.Opened)} {win.Pressure}");
                    //return $"{string.Join(',', win.Opened)} {win.Pressure}";
                    //await Task.Delay(1);
                }
            }
            return "bad";

        }
        public string SolveSecondPart(string inp)
        {
            var r = new Regex(@"Valve (..) has flow rate=(\d+); tunnels? leads? to valves? (.+)");
            var input = inp.Split("\n")
                .Select(x => r.Matches(x))
                .Select(x => new
                {
                    Valve = x[0].Groups[1].Value,
                    Rate = int.Parse(x[0].Groups[2].Value),
                    Valves = x[0].Groups[3].Value.Replace(", ", ",").Split(',')
                })
                .ToList();

            var rates = input.ToDictionary(x => x.Valve, x => x.Rate);
            // compute distances
            var distances = new Dictionary<string, Dictionary<string, int>>();
            foreach (var o in input.Where(x => x.Rate != 0 || x.Valve == "AA").Select(x => x.Valve))
            {
                var dico = new Dictionary<string, int>();
                var q = new Queue<string>();
                var done = new HashSet<string>();
                q.Enqueue(o);
                var d = 0;
                while (q.Count > 0)
                {
                    d++;
                    var newQueue = new Queue<string>();
                    while (q.Count > 0)
                    {
                        var node = q.Dequeue();
                        done.Add(node);
                        foreach (var next in input.Single(x => x.Valve == node).Valves)
                            if (!done.Contains(next))
                            {
                                if (rates[next] > 0 && !dico.ContainsKey(next)) dico.Add(next, d);
                                newQueue.Enqueue(next);
                            }
                    }
                    q = newQueue;
                }
                distances.Add(o, dico);
            }
            // Second test elephant à liste unique
            //// cost function with 2 actors
            var costFunctionE = (List<string> test) =>
            {
                var score = 0;
                {
                    var (a, ta) = ("AA", 26);
                    var (b, tb) = ("AA", 26);
                    foreach (var s in test)
                    {
                        var da = distances[a][s];
                        var db = distances[b][s];
                        var ta2 = ta - (da + 1);
                        var tb2 = tb - (db + 1);
                        var gainA = ta2 > 0 ? ta2 * rates[s] : 0;
                        var gainB = tb2 > 0 ? tb2 * rates[s] : 0;
                        if (gainA > gainB)
                        {
                            a = s;
                            ta = ta2;
                            score += gainA;
                        }
                        else
                        {
                            b = s;
                            tb = tb2;
                            score += gainB;
                        }
                    }
                }
                return score;
            };
            //Console.WriteLine($"SCORE TEST2 {costFunctionE(test)}");
            var rand = new Random();
            var score = 0;
            bool found = false;
            while (true)
            {
                var guess = input.Where(x => x.Rate > 0).Select(x => x.Valve).OrderBy(x => rand.Next()).ToList();
                while (!found)
                {
                    found = true;
                    for (var x = 0; x < guess.Count - 1; x++)
                        for (var y = x + 1; y < guess.Count; y++)
                        {
                            var tmp = guess[x];
                            guess[x] = guess[y];
                            guess[y] = tmp;
                            var s = costFunctionE(guess);
                            if (s > score)
                            {
                                score = s;
                                found = false;
                                return $"BEST {score} " + string.Join(',', guess);
                                //break;
                            }
                            else
                            {
                                tmp = guess[x];
                                guess[x] = guess[y];
                                guess[y] = tmp;
                            }
                        }
                }
            }
                
        }
        public IEnumerable<string> Part2bAsync(string inp)
        {
            var r = new Regex(@"Valve (..) has flow rate=(\d+); tunnels? leads? to valves? (.+)");
            var input = inp.Split("\n")
                .Select(x => r.Matches(x))
                .Select(x => new
                {
                    Valve = x[0].Groups[1].Value,
                    Rate = int.Parse(x[0].Groups[2].Value),
                    Valves = x[0].Groups[3].Value.Replace(", ", ",").Split(',')
                })
                .ToList();

            var visitors = new Queue<PipeVisitor>();
            var scores = new List<PipeVisitor>();
            var score = 0;
            visitors.Enqueue(new PipeVisitor
            {
                Position = "AA",
                PositionElephant = "AA",
                Journal = new List<string>(),
                JournalElephant = new List<string>(),
                Opened = new List<string>(),
                Pressure = 0
            });
            var ignore = input.Where(x => x.Rate == 0).Select(x => x.Valve).ToList();
            var valvesToOpen = input.Count - ignore.Count;
            var i = 28;
            var maxPressure = 0;
            while (visitors.Count > 0 && i-- > 0)
            {
                Console.WriteLine($"Iteration {i} queue lenght {visitors.Count}");
                // Manages pressure increase on each item
                var newQueue = new Queue<PipeVisitor>();
                while (visitors.Count > 0)
                {
                    var visitor = visitors.Dequeue();
                    var rate = input.Where(x => visitor.Opened.Contains(x.Valve)).Select(x => x.Rate).Sum();
                    visitor.Pressure += rate;
                    if (visitor.Pressure > maxPressure)
                        maxPressure = visitor.Pressure;
                    //if (visitor.Pressure < maxPressure / 3 ) // reduction of cases
                    //    continue;
                    // My turn
                    var forks = new List<PipeVisitor>();
                    var item = input.Find(x => x.Valve == visitor.Position) ?? throw new ArgumentNullException();
                    visitor.Journal.Add(visitor.Position);
                    // choice to open the valve
                    if (!ignore.Contains(visitor.Position) && !visitor.Opened.Contains(visitor.Position))
                    {
                        var newOpened = visitor.Opened.GetRange(0, visitor.Opened.Count);
                        newOpened.Add(visitor.Position);
                        var newVisitor = new PipeVisitor
                        {
                            Position = visitor.Position,
                            PositionElephant = visitor.PositionElephant,
                            Pressure = visitor.Pressure,
                            Journal = new List<string>(),
                            JournalElephant = visitor.JournalElephant.GetRange(0, visitor.JournalElephant.Count),
                            Opened = newOpened
                        };
                        if (newVisitor.Opened.Count == valvesToOpen)
                        {
                            //Console.WriteLine($"Visitor at {newVisitor.Position} pressure {newVisitor.Pressure} no more valves to open.");
                            //Console.WriteLine($"Rate {rate} and {i} {i*rate}");
                            rate = input.Where(x => newVisitor.Opened.Contains(x.Valve)).Select(x => x.Rate).Sum();
                            newVisitor.Pressure += i * rate;
                            if (newVisitor.Pressure >= score)
                            {
                                score = newVisitor.Pressure;
                                //Console.WriteLine($"Visitor at {newVisitor.Position} pressure Rate {rate} END {newVisitor.Pressure} opened {string.Join(',', newVisitor.Opened)}.");
                                newVisitor.Journal = visitor.Journal.GetRange(0, visitor.Journal.Count);
                                scores.Add(newVisitor);
                            }
                        }
                        else
                            forks.Add(newVisitor);
                    }
                    // choice to move to another valve
                    foreach (var child in item.Valves)
                        if (!visitor.Journal.Contains(child))
                            forks.Add(new PipeVisitor
                            {
                                Position = child,
                                PositionElephant = visitor.PositionElephant,
                                Pressure = visitor.Pressure,
                                Journal = visitor.Journal.GetRange(0, visitor.Journal.Count),
                                JournalElephant = visitor.JournalElephant.GetRange(0, visitor.JournalElephant.Count),
                                Opened = visitor.Opened.GetRange(0, visitor.Opened.Count)
                            });
                    foreach (var elephantTurn in forks)
                    {
                        var itemElephant = input.Find(x => x.Valve == elephantTurn.PositionElephant) ?? throw new ArgumentNullException();
                        elephantTurn.JournalElephant.Add(elephantTurn.PositionElephant);
                        // choice to open the valve
                        if (!ignore.Contains(elephantTurn.Position) && !elephantTurn.Opened.Contains(elephantTurn.Position))
                        {
                            var newOpened = elephantTurn.Opened.GetRange(0, elephantTurn.Opened.Count);
                            newOpened.Add(elephantTurn.Position);
                            var newVisitor = new PipeVisitor
                            {
                                Position = elephantTurn.Position,
                                PositionElephant = elephantTurn.PositionElephant,
                                Pressure = elephantTurn.Pressure,
                                Journal = elephantTurn.Journal.GetRange(0, elephantTurn.Journal.Count),
                                JournalElephant = new List<string>(),
                                Opened = newOpened
                            };
                            if (newVisitor.Opened.Count == valvesToOpen)
                            {
                                rate = input.Where(x => newVisitor.Opened.Contains(x.Valve)).Select(x => x.Rate).Sum();
                                newVisitor.Pressure += i * rate;
                                if (newVisitor.Pressure >= score)
                                {
                                    score = newVisitor.Pressure;
                                    //Console.WriteLine($"Visitor at {newVisitor.Position} pressure Rate {rate} END {newVisitor.Pressure} opened {string.Join(',', newVisitor.Opened)}.");
                                    newVisitor.Journal = visitor.Journal.GetRange(0, visitor.Journal.Count);
                                    scores.Add(newVisitor);
                                }
                            }
                            else
                                newQueue.Enqueue(newVisitor);
                        }
                        // choice to move to another valve
                        foreach (var child in itemElephant.Valves)
                            if (!elephantTurn.Journal.Contains(child))
                                newQueue.Enqueue(new PipeVisitor
                                {
                                    Position = elephantTurn.Position,
                                    PositionElephant = child,
                                    Pressure = elephantTurn.Pressure,
                                    Journal = elephantTurn.Journal.GetRange(0, elephantTurn.Journal.Count),
                                    JournalElephant = elephantTurn.JournalElephant.GetRange(0, elephantTurn.JournalElephant.Count),
                                    Opened = elephantTurn.Opened.GetRange(0, elephantTurn.Opened.Count)
                                });




                        // newQueue.Enqueue(elephantTurn);
                    }
                }
                visitors = newQueue;
                // Elephan's turn
                newQueue = new Queue<PipeVisitor>();

                if (visitors.Count > 0)
                {
                    var win = visitors.OrderByDescending(x => x.Pressure).First();
                    Console.WriteLine($"{string.Join(',', win.Opened)} {win.Pressure}");
                    Console.WriteLine($"Best Visitor at {win.Position}, elephant at {win.PositionElephant} pressure {win.Pressure} opened {string.Join(',', win.Opened)}.");
                    //yield return $"Best Visitor at {win.Position}, elephant at {win.PositionElephant} pressure {win.Pressure} opened {string.Join(',', win.Opened)}.";
                    //await Task.Delay(1);
                }
                if (scores.Count > 0)
                {
                    var win = scores.OrderByDescending(x => x.Pressure).First();
                    Console.WriteLine($"{string.Join(',', win.Opened)} {win.Pressure}");
                    Console.WriteLine($"SCORE Visitor at {win.Position}, elephant at {win.PositionElephant} pressure {win.Pressure} opened {string.Join(',', win.Opened)}.");
                    yield return $"SCORE Visitor at {win.Position}, elephant at {win.PositionElephant} pressure {win.Pressure} opened {string.Join(',', win.Opened)}.";
                }
            }
        }
    }
}
