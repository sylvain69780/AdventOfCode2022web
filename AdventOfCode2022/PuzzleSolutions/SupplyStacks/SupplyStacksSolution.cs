using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode2022Solutions.PuzzleSolutions;

namespace AdventOfCode2022Solutions.PuzzleSolutions.SupplyStacks
{
    public class SupplyStacksSolution : IPuzzleSolution
    {
        private string _puzzleInput = string.Empty;
        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }
        private static string[] ToLines(string s) => s.Split("\n");

        private static Stack<char>[] ReadStacks(string puzzleInput)
        {
            var records = ToLines(puzzleInput).AsEnumerable().GetEnumerator();
            records.MoveNext();
            var stacks = new Stack<char>[1 + records.Current.Length / 4];
            for (int i = 0; i < stacks.Length; i++)
                stacks[i] = new Stack<char>();

            var rows = new Stack<string>();
            while (records.Current[1] != '1')
            {
                rows.Push(records.Current);
                records.MoveNext();
            }
            foreach (var row in rows)
                for (int stackIdx = 0; stackIdx < stacks.Length; stackIdx++)
                    if (row[stackIdx * 4 + 1] != ' ')
                        stacks[stackIdx].Push(row[stackIdx * 4 + 1]);
            return stacks;
        }

        private static List<(int Count, int From, int To)> ReadMovesToDo(string puzzleInput)
        {
            var records = ToLines(puzzleInput).AsEnumerable().GetEnumerator();
            records.MoveNext(); // skip until blank separator line
            while (records.Current != "")
                records.MoveNext();
            var moves = new List<(int Move, int From, int To)>();
            var regex = new Regex(@"move (\d+) from (\d+) to (\d+)", RegexOptions.Compiled);
            while (records.MoveNext())
            {
                var g = regex.Match(records.Current).Groups;
                moves.Add((int.Parse(g[1].Value), int.Parse(g[2].Value), int.Parse(g[3].Value)));
            }
            return moves;
        }

        public IEnumerable<string> SolveFirstPart()
        {
            var stacks = ReadStacks(_puzzleInput);
            var moves = ReadMovesToDo(_puzzleInput);
            foreach (var (count, from, to) in moves)
            {
                for (var i = 0; i < count; i++)
                {
                    var c = stacks[from - 1].Pop();
                    stacks[to - 1].Push(c);
                }
            }
            yield return string.Join("", stacks.Select(x => x.Count == 0 ? ' ' : x.Peek()));
        }
        public IEnumerable<string> SolveSecondPart()
        {
            var stacks = ReadStacks(_puzzleInput);
            var moves = ReadMovesToDo(_puzzleInput);
            var tmp = new Stack<char>();
            foreach (var (count, from, to) in moves)
            {
                for (int i = 0; i < count; i++)
                    tmp.Push(stacks[from - 1].Pop());
                for (int i = 0; i < count; i++)
                    stacks[to - 1].Push(tmp.Pop());
            }
            yield return string.Join("", stacks.Select(x => x.Count == 0 ? ' ' : x.Peek()));
        }
    }
}