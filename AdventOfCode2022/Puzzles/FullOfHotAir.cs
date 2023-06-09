﻿namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(25, "Full of Hot Air")]
    public class FullOfHotAir : IPuzzleSolver
    {
        public string SolveFirstPart(string inp)
        {
            var input = inp.Split("\n");
            var values = new char[] { '=', '-', '0', '1', '2' };
            var result = 0L;
            foreach (var line in input)
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
            return string.Concat(snafu);
        }
        public string SolveSecondPart(string inp)
        {
            return "The second part of the Challenge was to solve all Puzzles !";
        }
    }
}
