using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.GrovePositioningSystem
{
    public class GrovePositioningSystemModel : IPuzzleModel
    {
        List<(int Id, long Number)>? _arrangement;
        public List<(int Id, long Number)>? Arrangement => _arrangement;
        public void Parse(string input)
        {
            _arrangement = LoadArrangement(input);
        }
        private static List<(int Id, long Number)> LoadArrangement(string puzzleInput)
        {
            var c = 0;
            var arrangement = puzzleInput.Split("\n").Select(x => (Id: c++, Number: long.Parse(x))).ToList();
            return arrangement;
        }
        public static void Mix(List<(int Id, long Number)> arrangement)
        {
            for (var id = 0; id < arrangement.Count; id++)
            {
                var numberPosition = arrangement.FindIndex(x => x.Id == id);
                var numberToMove = arrangement[numberPosition];
                if (numberToMove.Number == 0)
                    continue;
                arrangement.RemoveAt(numberPosition);
                var targetPosition = (int)((numberPosition + numberToMove.Number) % arrangement.Count);
                if (numberToMove.Number > 0)
                    arrangement.Insert(targetPosition, numberToMove);
                else
                {
                    if (targetPosition < 0)
                        targetPosition += arrangement.Count;
                    if (targetPosition == 0)
                        arrangement.Add(numberToMove);
                    else
                        arrangement.Insert(targetPosition, numberToMove);
                }
            }
        }

        public static long DecodeGroveCoordinates(List<(int Id, long Number)> arrangement)
        {
            var groveCoordinates = 0L;
            for (var position = 1000; position <= 3000; position += 1000)
            {
                var zero = arrangement.FindIndex(x => x.Number == 0);
                var index = (position + zero) % arrangement.Count;
                groveCoordinates += arrangement[index].Number;
            }
            return groveCoordinates;
        }

    }
}
