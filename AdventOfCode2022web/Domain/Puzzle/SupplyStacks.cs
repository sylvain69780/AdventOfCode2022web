using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2022web.Domain.Puzzle
{
    public class SupplyStacks : PuzzleSolver
    {
        private static string[] ToLines(string s) => s.Split("\n");

        private (Stack<char>[],List<(int Count,int From, int To)>) ReadStacksAndMoves(string puzzleInput)
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
            {
                for (int stackIdx = 0; stackIdx < stacks.Length; stackIdx++)
                    if (row[stackIdx * 4 + 1] != ' ')
                        stacks[stackIdx].Push(row[stackIdx * 4 + 1]);
            }

            records.MoveNext(); // skip blank separator line
            var moves = new List<(int Move, int From, int To)>();
            var regex = new Regex(@"move (\d+) from (\d+) to (\d+)",RegexOptions.Compiled);
            while (records.MoveNext())
            {
                var g = regex.Match(records.Current).Groups;
                moves.Add((int.Parse(g[1].Value), int.Parse(g[2].Value) , int.Parse(g[3].Value) ));
            }
            return (stacks,moves);
        }

        protected override string SolveFirst(string puzzleInput)
        {
            var (stacks,moves) = ReadStacksAndMoves(puzzleInput);
            foreach(var (count, from, to) in moves)
            {
                for (var i = 0; i< count;i++)
                {
                    var c = stacks[from-1].Pop();
                    stacks[to-1].Push(c);
                }
            }
            return string.Join("", stacks.Select(x => x.FirstOrDefault(' ')));
        }
        protected override string SolveSecond(string puzzleInput)
        {
            var (stacks, moves) = ReadStacksAndMoves(puzzleInput);
            var tmp = new Stack<char>();
            foreach (var (count, from, to) in moves)
            {
                for (int i = 0; i < count; i++)
                    tmp.Push(stacks[from - 1].Pop());
                for (int i = 0; i < count; i++)
                    stacks[to - 1].Push(tmp.Pop());
            }
            return string.Join("", stacks.Select(x => x.FirstOrDefault(' ')));
        }
    }
}