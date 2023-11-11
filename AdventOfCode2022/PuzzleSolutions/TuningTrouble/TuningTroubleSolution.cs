using AdventOfCode2022Solutions.PuzzleSolutions;

namespace AdventOfCode2022Solutions.PuzzleSolutions.TuningTrouble
{
    public class TuningTroubleSolution : IPuzzleSolution
    {
        private string _puzzleInput = string.Empty;
        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }

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

        public IEnumerable<string> SolveFirstPart()
        {
            yield return Format(FindMarkerPosition(_puzzleInput, 4));
        }
        public IEnumerable<string> SolveSecondPart()
        {
            yield return Format(FindMarkerPosition(_puzzleInput, 14));
        }
    }
}