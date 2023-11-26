using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TuningTrouble
{
    public class TuningTroubleModel : IPuzzleModel
    {
        string _puzzleInput = string.Empty;
        public void Parse(string input)
        {
            _puzzleInput = input;
        }

        public int FindMarkerPosition(int sequenceLenght)
        {
            var marker = new Queue<char>();
            var processedCharacters = 0;
            foreach (var c in _puzzleInput)
            {
                processedCharacters++;
                if (marker.Count == sequenceLenght) marker.Dequeue();
                marker.Enqueue(c);
                if (marker.Count == sequenceLenght && marker.GroupBy(x => x).Select(y => y.Count()).Max() == 1)
                    break;
            }
            return processedCharacters;
        }
    }
}
