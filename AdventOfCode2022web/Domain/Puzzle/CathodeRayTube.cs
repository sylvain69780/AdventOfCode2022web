using System.Text;

namespace AdventOfCode2022web.Domain.Puzzle
{
    public class CathodeRayTube : IPuzzleSolver
    {
        public async IAsyncEnumerable<string> Part1Async(string input)
        {
            Input = input;
            yield return Part1();
            await Task.Delay(1);
        }
        public async IAsyncEnumerable<string> Part2Async(string input)
        {
            Input = input;
            yield return Part2();
            await Task.Delay(1);
        }

        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            var input = Input.Split("\n");
            var x = input.Select(x => x.Split(" "))
                .SelectMany(x => x[0] == "noop" ? new int[] { 0 } : new int[] { 0, int.Parse(x[1]) });
            var X = 1;
            var cycles = 20;
            var c = 0;
            var s = 0;
            foreach (var i in x)
            {
                c++;
                if (c == cycles)
                {
                    cycles += 40;
                    s += X * c;
                }
                X += i;
            }
            return s.ToString();
        }
        public string Part2()
        {
            var input = Input.Split("\n");
            var seq = input.Select(x => x.Split(" "))
                .SelectMany(x => x[0] == "noop" ? new int[] { 0 } : new int[] { 0, int.Parse(x[1]) });
            var X = 1;
            var c = 0;
            var seq2 = seq.Select(x => (c: ++c, X, X2: X += x)).GetEnumerator();
            var line = new StringBuilder();
            var res = new List<string>();
            foreach (var y in Enumerable.Range(0, 6))
            {
                line.Clear();
                foreach (var x in Enumerable.Range(0, 40))
                    if (seq2.MoveNext() && seq2.Current.X >= x - 1 && seq2.Current.X <= x + 1)
                        line.Append('#');
                    else
                        line.Append('.');
                Console.WriteLine(line);
                res.Add(line.ToString());
            }
            return string.Join("\n", res);
        }
    }
}