namespace AdventOfCode2022web.Domain.Puzzle
{
    public class SupplyStacks : PuzzleSolver
    {
        protected override string Part1(string inp)
        {
            var input = inp.Split("\n").AsEnumerable().GetEnumerator();
            input.MoveNext();
            var w = 1 + (input.Current.Length) / 4;
            var stacks = new Stack<char>[w];
            foreach (var i in Enumerable.Range(0, w))
                stacks[i] = new Stack<char>();
            while (input.Current[1] != '1')
            {
                foreach (var i in Enumerable.Range(0, w))
                    if (input.Current[i * 4 + 1] != ' ')
                        stacks[i].Push(input.Current[i * 4 + 1]);
                input.MoveNext();
            }
            input.MoveNext();
            // reversing the stacks because of read order of the input :-(
            foreach (var i in Enumerable.Range(0, w))
            {
                var l = new Stack<char>();
                foreach (var c in stacks[i])
                    l.Push(c);
                stacks[i] = l;
            }
            while (input.MoveNext())
            {
                Console.WriteLine(input.Current);
                var i = input.Current.Replace("move ", "").Replace(" from ", ",").Replace(" to ", ",").Split(',').Select(x => int.Parse(x)).ToList();
                var (amount, origin, destination) = (i[0], i[1], i[2]);
                foreach (var c in Enumerable.Range(1, amount))
                {
                    stacks[destination - 1].Push(stacks[origin - 1].Pop());
                    Console.WriteLine(string.Join("", stacks.Select(x => x.FirstOrDefault(' '))));
                }
            }
            return string.Join("", stacks.Select(x => x.FirstOrDefault(' ')));
        }
        protected override string Part2(string inp)
        {
            var input = inp.Split("\n").AsEnumerable().GetEnumerator();
            input.MoveNext();
            var w = 1 + (input.Current.Length) / 4;
            var stacks = new Stack<char>[w];
            foreach (var i in Enumerable.Range(0, w))
                stacks[i] = new Stack<char>();
            while (input.Current[1] != '1')
            {
                foreach (var i in Enumerable.Range(0, w))
                    if (input.Current[i * 4 + 1] != ' ')
                        stacks[i].Push(input.Current[i * 4 + 1]);
                input.MoveNext();
            }
            input.MoveNext();
            // reversing the stacks because of read order of the input :-(
            foreach (var i in Enumerable.Range(0, w))
            {
                var l = new Stack<char>();
                foreach (var c in stacks[i])
                    l.Push(c);
                stacks[i] = l;
            }
            while (input.MoveNext())
            {
                Console.WriteLine(input.Current);
                var i = input.Current.Replace("move ", "").Replace(" from ", ",").Replace(" to ", ",").Split(',').Select(x => int.Parse(x)).ToList();
                var (amount, origin, destination) = (i[0], i[1], i[2]);
                var mover = new Stack<char>();
                foreach (var c in Enumerable.Range(1, amount))
                    mover.Push(stacks[origin - 1].Pop());
                foreach (var c in mover)
                    stacks[destination - 1].Push(c);
                Console.WriteLine(string.Join("", stacks.Select(x => x.FirstOrDefault(' '))));
            }
            return string.Join("", stacks.Select(x => x.FirstOrDefault(' ')));
        }
    }
}