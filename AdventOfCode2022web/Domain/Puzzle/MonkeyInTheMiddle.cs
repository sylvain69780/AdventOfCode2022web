namespace AdventOfCode2022web.Domain.Puzzle
{
    public class MonkeyInTheMiddle : IPuzzleSolver
    {
        class Monkey
        {
            public List<long> Items = new();
            public string[] Oper = new string[3];
            public long Test;
            public int IfTrue;
            public int IfFalse;
            public long Inspections;
        }

        public IEnumerable<string> SolveFirstPart(string inp)
        {
            var input = inp.Split("\n").AsEnumerable().GetEnumerator();
            var ms = new List<Monkey>();
            while (input.MoveNext())
            {
                input.MoveNext();
                var items = input.Current.Replace("  Starting items: ", "").Split(',').Select(x => long.Parse(x)).ToList();
                input.MoveNext();
                var oper = input.Current.Replace("  Operation: new = ", "").Split(' ');
                input.MoveNext();
                var test = int.Parse(input.Current.Replace("  Test: divisible by ", ""));
                input.MoveNext();
                var ifTrue = int.Parse(input.Current.Replace("    If true: throw to monkey ", ""));
                input.MoveNext();
                var ifFalse = int.Parse(input.Current.Replace("   If false: throw to monkey ", ""));
                input.MoveNext();
                var m = new Monkey
                {
                    Items = items,
                    Oper = oper,
                    Test = test,
                    IfFalse = ifFalse,
                    IfTrue = ifTrue
                };
                ms.Add(m);
            }
            long score = 0;
            foreach (var round in Enumerable.Range(1, 20))
            {
                foreach (var m in ms)
                {
                    Console.WriteLine($"Monkey {ms.IndexOf(m)}");
                    m.Inspections += m.Items.Count;
                    foreach (var i in m.Items)
                    {
                        Console.WriteLine($"  Monkey inspects an item with a worry level of {i}.");
                        var op = m.Oper[1] == "*" ? "muliplied" : "increased";
                        var arg = m.Oper[2] == "old" ? "itself" : m.Oper[2];
                        var o = m.Oper[2] == "old" ? i : int.Parse(m.Oper[2]);
                        var newVal = m.Oper[1] == "*" ? i * o : i + o;
                        Console.WriteLine($"    Worry level is {op} by {arg} to {newVal}.");
                        newVal /= 3;
                        Console.WriteLine($"    Monkey gets bored with item. Worry level is divided by 3 to {newVal}");
                        if (newVal % m.Test == 0)
                            ms[m.IfTrue].Items.Add(newVal);
                        else
                            ms[m.IfFalse].Items.Add(newVal);
                    }
                    m.Items.Clear();
                }
                foreach (var m in ms)
                    Console.WriteLine("items " + String.Join(',', m.Items));
                foreach (var m in ms)
                    Console.WriteLine("Inspections " + m.Inspections.ToString());
                score = ms.Select(x => x.Inspections).OrderByDescending(x => x).Take(2).Aggregate((long)1, (x, y) => y * x);
                Console.WriteLine("Score: " + score);
            }
            yield  return score.ToString();
        }
        public IEnumerable<string> SolveSecondPart(string inp)
        {
            var input = inp.Split("\n").AsEnumerable().GetEnumerator();
            var ms = new List<Monkey>();
            while (input.MoveNext())
            {
                input.MoveNext();
                var items = input.Current.Replace("  Starting items: ", "").Split(',').Select(x => long.Parse(x)).ToList();
                input.MoveNext();
                var oper = input.Current.Replace("  Operation: new = ", "").Split(' ');
                input.MoveNext();
                var test = int.Parse(input.Current.Replace("  Test: divisible by ", ""));
                input.MoveNext();
                var ifTrue = int.Parse(input.Current.Replace("    If true: throw to monkey ", ""));
                input.MoveNext();
                var ifFalse = int.Parse(input.Current.Replace("   If false: throw to monkey ", ""));
                input.MoveNext();
                var m = new Monkey
                {
                    Items = items,
                    Oper = oper,
                    Test = test,
                    IfFalse = ifFalse,
                    IfTrue = ifTrue
                };
                ms.Add(m);
            }
            var bigDiv = ms.Select(x => x.Test).Aggregate(1L, (x, y) => y * x);
            foreach (var round in Enumerable.Range(1, 10000))
            {
                foreach (var m in ms)
                {
                    m.Inspections += m.Items.Count;
                    foreach (var i in m.Items)
                    {
                        var newVal = i;
                        if (m.Oper[1] == "*")
                        {
                            if (m.Oper[2] == "old")
                            {
                                newVal *= newVal;
                            }
                            else
                            {
                                var o = int.Parse(m.Oper[2]);
                                newVal *= o;
                            }
                        }
                        else
                        {
                            var o = int.Parse(m.Oper[2]);
                            newVal += o;
                        }
                        newVal = newVal % bigDiv;
                        if (newVal % m.Test == 0)
                            ms[m.IfTrue].Items.Add(newVal);
                        else
                            ms[m.IfFalse].Items.Add(newVal);
                    }
                    m.Items.Clear();
                }
                //foreach (var m in ms)
                //    Console.WriteLine("items " + String.Join(',', m.Items));
                //foreach (var m in ms)
                //    Console.WriteLine("Inspections " + m.Inspections.ToString());
            }
            foreach (var m in ms)
                Console.WriteLine("Inspections " + m.Inspections.ToString());
            var score = ms.Select(x => x.Inspections).OrderByDescending(x => x).Take(2).Aggregate(1L, (x, y) => y * x);
            Console.WriteLine("Score: " + score);
            yield return score.ToString();
        }
    }
}