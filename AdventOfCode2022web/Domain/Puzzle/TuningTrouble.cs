namespace AdventOfCode2022web.Domain.Puzzle
{
    public class TuningTrouble : PuzzleSolver
    {
        private static string Format(int v) => v.ToString();

        protected override string SolveFirst(string puzzleInput)
        {
            var marker = new Queue<char>();
            var processedCharacters = 0;
            foreach (var c in puzzleInput)
            {
                processedCharacters++;
                if (marker.Count == 4) marker.Dequeue();
                marker.Enqueue(c);
                if (marker.Count == 4 && marker.GroupBy(x => x).Select(y => y.Count()).Max() == 1) break;
            }
            return Format(processedCharacters);
        }
        protected override string SolveSecond(string puzzleInput)
        {
            var marker = new Queue<char>();
            var processedCharacters = 0;
            foreach (var c in puzzleInput)
            {
                processedCharacters++;
                if (marker.Count == 14) marker.Dequeue();
                marker.Enqueue(c);
                if (marker.Count == 14 && marker.GroupBy(x => x).Select(y => y.Count()).Max() == 1) break;
            }
            return Format(processedCharacters);
        }
    }
}