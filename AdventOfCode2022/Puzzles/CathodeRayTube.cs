﻿using System.Text;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(10, "Cathode Ray Tube")]
    public class CathodeRayTube : IPuzzleSolver
    {
        private static string Format(int v) => v.ToString();
        private static string[] ToLines(string s) => s.Split("\n");

        public string SolveFirstPart(string puzzleInput)
        {
            var program = ToLines(puzzleInput);
            var numsToAdd = program.Select(x => x.Split(" "))
                .SelectMany(x => x[0] == "noop" ? new int[] { 0 } : new int[] { 0, int.Parse(x[1]) });
            var valueOfXregister = 1;
            var cycleToRecord = 20;
            var currentCycle = 0;
            var sumOfSixSignalStrengths = 0;
            foreach (var value in numsToAdd)
            {
                currentCycle++;
                if (currentCycle == cycleToRecord)
                {
                    cycleToRecord += 40;
                    sumOfSixSignalStrengths += valueOfXregister * currentCycle;
                }
                valueOfXregister += value;
            }
            return Format(sumOfSixSignalStrengths);
        }

        private static IEnumerable<int> ComputeValuesOfXRegister(IEnumerable<int> numsToAdd)
        {
            var valueOfXregister = 1;
            var currentCycle = 0;
            foreach (var value in numsToAdd)
            {
                yield return valueOfXregister;
                currentCycle++;
                valueOfXregister += value;
            }
        }

        public string SolveSecondPart(string puzzleInput)
        {
            var program = ToLines(puzzleInput);
            var numsToAdd = program.Select(x => x.Split(" "))
                .SelectMany(x => x[0] == "noop" ? new int[] { 0 } : new int[] { 0, int.Parse(x[1]) });
            var valuesOfXRegister = ComputeValuesOfXRegister(numsToAdd).GetEnumerator();
            var messageLine = new StringBuilder();
            var message = new List<string>();
            foreach (var _ in Enumerable.Range(0, 6))
            {
                messageLine.Clear();
                foreach (var pixelPosX in Enumerable.Range(0, 40))
                    if (valuesOfXRegister.MoveNext() && valuesOfXRegister.Current >= pixelPosX - 1 && valuesOfXRegister.Current <= pixelPosX + 1)
                        messageLine.Append('#');
                    else
                        messageLine.Append('.');
                message.Add(messageLine.ToString());
            }
            return string.Join("\n", message);
        }
    }
}