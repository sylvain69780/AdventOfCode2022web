namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(25, "Full of Hot Air")]
    public class FullOfHotAir : IPuzzleSolverV3
    {
        public void Setup(string inp)
        {
            input = inp.Split("\n");
        }

        private string[]? input;

        private static readonly char[] values = new char[] { '=', '-', '0', '1', '2' };

        public IEnumerable<string> SolveFirstPart()
        {
            var result = 0L;
            foreach (var line in input!)
            {
                var b = 1L;
                var res = 0L;
                foreach (var c in line.Reverse())
                {
                    var v = (long)Array.IndexOf(values, c) - 2;
                    res += v * b;
                    b *= 5;
                }
                result += res;
            }
            Console.WriteLine(result);
            var snafu = new Stack<char>();
            var num = result;
            while (num != 0)
            {
                var rem = num % 5L;
                snafu.Push(values[(rem + 2) % 5]);
                var addUp = rem > 2 ? 1 : 0;
                num /= 5L;
                num += addUp;
            }
            yield return string.Concat(snafu);
        }
        public IEnumerable<string> SolveSecondPart()
        {
            yield return "The second part of the Challenge was to solve all Puzzles !";
        }
    }
}
