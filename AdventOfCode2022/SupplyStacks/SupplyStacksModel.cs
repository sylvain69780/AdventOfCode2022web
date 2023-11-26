using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.SupplyStacks
{
    public class SupplyStacksModel : IPuzzleModel
    {
        Stack<char>[]? _stacks;
        public Stack<char>[]? Stacks => _stacks;
        List<(int Count, int From, int To)>? _movesToDo;
        public List<(int Count, int From, int To)>? MovesToDo => _movesToDo;

        private static Stack<char>[] ReadStacks(string puzzleInput)
        {
            var records = puzzleInput.Split("\n").AsEnumerable().GetEnumerator();
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
            var records = puzzleInput.Split("\n").AsEnumerable().GetEnumerator();
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
        public void Parse(string input)
        {
            input = input.Replace("\r", "");
            _stacks = ReadStacks(input);
            _movesToDo = ReadMovesToDo(input);
        }
    }
}
