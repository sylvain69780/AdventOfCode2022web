﻿namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(6, "Tuning Trouble")]
    public class TuningTrouble : IPuzzleSolver
    {
        private static string Format(int v) => v.ToString();

        private static int FindMarkerPosition(string puzzleInput, int sequenceLenght)
        {
            var marker = new Queue<char>();
            var processedCharacters = 0;
            foreach (var c in puzzleInput)
            {
                processedCharacters++;
                if (marker.Count == sequenceLenght) marker.Dequeue();
                marker.Enqueue(c);
                if (marker.Count == sequenceLenght && marker.GroupBy(x => x).Select(y => y.Count()).Max() == 1)
                    break;
            }
            return processedCharacters;
        }

        public string SolveFirstPart(string puzzleInput)
        {
             return Format(FindMarkerPosition(puzzleInput, 4));
        }
        public string SolveSecondPart(string puzzleInput)
        {
             return Format(FindMarkerPosition(puzzleInput, 14));
        }
    }
}