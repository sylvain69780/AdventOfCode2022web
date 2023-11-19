namespace Domain.GrovePositioningSystem
{
    public class GrovePositioningSystemSolution : IPuzzleSolution
    {
        private string _puzzleInput = string.Empty;
        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }

        public IEnumerable<string> SolveFirstPart()
        {
            var arrangement = LoadArrangement(_puzzleInput);
            Mix(arrangement);
            var groveCoordinates = DecodeGroveCoordinates(arrangement);
            yield return groveCoordinates.ToString();
        }

        public IEnumerable<string> SolveSecondPart()
        {
            var arrangement = LoadArrangement(_puzzleInput);
            const long decryptionKey = 811589153;
            arrangement = arrangement.Select(x => (x.Id, x.Number * decryptionKey)).ToList();
            for (var turns = 0; turns < 10; turns++)
                Mix(arrangement);
            var groveCoordinates = DecodeGroveCoordinates(arrangement);
            yield return groveCoordinates.ToString();
        }

        private static List<(int Id, long Number)> LoadArrangement(string puzzleInput)
        {
            var c = 0;
            var arrangement = puzzleInput.Split("\n").Select(x => (Id: c++, Number: long.Parse(x))).ToList();
            return arrangement;
        }

        private static void Mix(List<(int Id, long Number)> arrangement)
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

        private static long DecodeGroveCoordinates(List<(int Id, long Number)> arrangement)
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