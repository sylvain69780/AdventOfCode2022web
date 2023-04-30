namespace AdventOfCode2022web.Domain.Puzzle
{
    public class TuningTrouble : IPuzzleSolver
    {
        private static string Format(int v) => v.ToString();

        public IEnumerable<string> SolveFirstPart(string puzzleInput)
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
            yield return Format(processedCharacters);
        }
        public IEnumerable<string> SolveSecondPart(string puzzleInput)
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
            yield return Format(processedCharacters);
        }
    }
}