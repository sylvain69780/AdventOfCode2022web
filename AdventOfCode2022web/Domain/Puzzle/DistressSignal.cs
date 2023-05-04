namespace AdventOfCode2022web.Domain.Puzzle
{
    [Puzzle(13, "Distress Signal")]
    public class DistressSignal : IPuzzleSolver
    {
        class Day13Element
        {
            public static Day13Element ReadInput(string inp)
            {
                if (inp[0] == '[')
                {
                    if (inp[1] == ']') return new Day13List();
                    var begin = 1;
                    var st = new List<string>();
                    var lvl = 0;
                    for (var end = 1; end < inp.Length - 2; end++)
                    {
                        if (inp[end] == ',' && lvl == 0)
                        {
                            st.Add(inp[begin..end]);
                            begin = end + 1;
                        }
                        if (inp[end] == '[') lvl++;
                        if (inp[end] == ']') lvl--;
                    }
                    st.Add(inp[begin..(inp.Length - 1)]);
                    return new Day13List
                    {
                        Lst = st.Select(x => ReadInput(x)).ToList()
                    };
                }
                else
                {
                    return new Day13Leaf() { Val = int.Parse(inp) };
                }
            }
            public static int IsOrdered(Day13Element? a, Day13Element? b)
            {
                if (a is Day13Leaf av && b is Day13Leaf bv)
                    return av.Val < bv.Val ? 1 : av.Val == bv.Val ? 0 : -1;
                if (a is Day13Leaf)
                    a = new Day13List { Lst = new List<Day13Element> { a } };
                if (b is Day13Leaf)
                    b = new Day13List { Lst = new List<Day13Element> { b } };
                Day13List? al = a as Day13List;
                Day13List? bl = b as Day13List;
                for (var i = 0; i < Math.Min(al?.Lst.Count ?? 0, bl?.Lst.Count ?? 0); i++)
                {
                    var va = al?.Lst[i];
                    var vb = bl?.Lst[i];
                    var res = IsOrdered(va, vb);
                    if (res != 0) return res;
                }
                return al?.Lst.Count < bl?.Lst.Count ? 1 : al?.Lst.Count == bl?.Lst.Count ? 0 : -1;
            }
        }
         class Day13Leaf : Day13Element
        {
            public int Val;
        }
         class Day13List : Day13Element
        {
            public List<Day13Element> Lst = new();
        }

        class MyComparer : IComparer<Day13Element>
        {

            public int Compare(Day13Element? A, Day13Element? B)
            {
                return Day13Element.IsOrdered(A, B);
            }

        }

        public IEnumerable<string> SolveFirstPart(string inp)
        {
            var input = inp.Split("\n").ToList().GetEnumerator();
            var idx = 0;
            var score = 0;
            while (input.MoveNext())
            {
                idx++;
                var p1 = Day13Element.ReadInput(input.Current);
                input.MoveNext();
                var p2 = Day13Element.ReadInput(input.Current);
                input.MoveNext();
                var r = Day13Element.IsOrdered(p1, p2);
                Console.WriteLine(r);
                if (r == 1) score += idx;
            }
            yield return score.ToString();
        }
        public IEnumerable<string> SolveSecondPart(string inp)
        {
            var input = inp.Split("\n").Where(x => x != "").Append("[[2]]").Append("[[6]]").Select(x => (a: x, b: Day13Element.ReadInput(x)))
                .OrderByDescending(x => x.b, new MyComparer()).Select(x => x.a).ToList();

            yield return((1 + input.IndexOf("[[2]]")) * (1 + input.IndexOf("[[6]]"))).ToString();
        }
    }
}