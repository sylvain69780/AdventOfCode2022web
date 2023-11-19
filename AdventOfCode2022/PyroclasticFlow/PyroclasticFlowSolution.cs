using System.Text;
namespace Domain.PyroclasticFlow
{
    public class PyroclasticFlowSolution : IPuzzleSolution
    {
        private string _puzzleInput = string.Empty;
        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }
        private class JetGenerator
        {
            public string JetPattern = "";
            public int Counter;
            public char FetchJetDirection()
            {
                var counter = Counter;
                Counter = (counter + 1) % JetPattern.Length;
                return JetPattern[counter];
            }
        }
        private class RockGenerator
        {
            private static readonly (int x, int y)[][] BlockShapes = new (int x, int y)[][]
                {
                new (int x, int y)[] {(0,0), (1,0), (2,0), (3,0)},          // horizontal bar
                new (int x, int y)[] {(1,0), (0,1), (1,1), (2,1), (1,2)},   // cross
                new (int x, int y)[] {(0,0), (1,0), (2,0), (2,1), (2,2)},   // L
                new (int x, int y)[] {(0,0), (0,1), (0,2), (0,3) },         // vertical bar
                new (int x, int y)[] {(0,0), (1,0), (0,1), (1,1) }          // square
                };

            public int Counter;

            public (int x, int y)[] FetchRockShape()
            {
                var counter = Counter;
                Counter = (counter + 1) % 5;
                return BlockShapes[counter];
            }
        }

        private string Visualize(HashSet<(int x, int y)> occupiedSlots, int highestPoint)
        {
            var sb = new StringBuilder();
            for (var y = 0; y < 10; y++)
            {
                sb.Append('#');
                for (var x = 0; x < 7; x++)
                {
                    sb.Append(occupiedSlots.Contains((x, highestPoint - y)) ? '@' : ' ');
                }
                sb.Append("#\n");
            }
            return sb.ToString();
        }

        public IEnumerable<string> SolveFirstPart()
        {
            var jetGenerator = new JetGenerator { JetPattern = _puzzleInput };
            var rockGenerator = new RockGenerator();
            var occupiedSlots = new HashSet<(int x, int y)>();
            int highestPoint = 0;
            const int maxIterations = 2022;
            for (var i = 0; i <= maxIterations; i++)
            {
                var rock = rockGenerator.FetchRockShape();
                highestPoint = occupiedSlots.Count == 0 ? 0 : occupiedSlots.Select(p => p.y).Max() + 1;
                var rockPosition = (x: 2, y: highestPoint + 3);
                var stop = false;
                while (!stop)
                {
                    var direction = jetGenerator.FetchJetDirection() == '>' ? 1 : -1;
                    if (!rock.Select(x => (x.Item1 + rockPosition.x + direction, x.Item2 + rockPosition.y)).Any(x => x.Item1 < 0 || x.Item1 >= 7 || occupiedSlots.Contains(x)))
                        rockPosition.x += direction;
                    if (rock.Select(p => (x: p.x + rockPosition.x, y: p.y + rockPosition.y - 1)).Any(p => p.y < 0 || occupiedSlots.Contains(p)))
                        break;
                    else
                        rockPosition.y--;
                }
                foreach (var (x, y) in rock)
                    occupiedSlots.Add((x + rockPosition.x, y + rockPosition.y));

            }
            Console.WriteLine(Visualize(occupiedSlots, highestPoint));
            yield return $"After {maxIterations} rocks fallen, the Tower highest point is at y={highestPoint}";
        }
        public IEnumerable<string> SolveSecondPart()
        {
            var jetGenerator = new JetGenerator { JetPattern = _puzzleInput };
            var rockGenerator = new RockGenerator();
            var occupiedPositions = new HashSet<(int x, int y)>();
            var fallenRocksRecording = new List<(int Height, string RockPosition)>();
            var outputIdx = new Dictionary<string, int>();
            var isCycleFound = false;
            var highestPoint = 0;
            var (cycleStart, cycleEnd) = (0, 1);
            var slidingWindowSize = 1;
            while (!isCycleFound)
            {
                var rock = rockGenerator.FetchRockShape();
                var rockPosition = (x: 2, y: 3);
                while (true)
                {
                    var direction = jetGenerator.FetchJetDirection() == '>' ? 1 : -1;
                    if (!rock.Select(p => (x: p.x + rockPosition.x + direction, y: p.y + rockPosition.y + highestPoint)).Any(p => p.x < 0 || p.x >= 7 || occupiedPositions.Contains(p)))
                        rockPosition.x += direction;
                    if (rock.Select(p => (x: p.x + rockPosition.x, y: p.y + rockPosition.y + highestPoint - 1)).Any(x => x.y < 0 || occupiedPositions.Contains(x)))
                        break;
                    rockPosition.y--;
                }
                foreach (var (x, y) in rock)
                    occupiedPositions.Add((x + rockPosition.x, y + rockPosition.y + highestPoint));
                highestPoint = occupiedPositions.Select(x => x.y).Max() + 1;
                fallenRocksRecording.Add((highestPoint, $"{rockGenerator.Counter} {jetGenerator.Counter} {rockPosition.x} {rockPosition.y}"));
                // look for cycles aglorithm
                if (fallenRocksRecording.Count > 1)
                {
                    if (fallenRocksRecording[^1].RockPosition == fallenRocksRecording[^(1 + slidingWindowSize)].RockPosition)
                    {
                        if (fallenRocksRecording.Count - cycleStart > slidingWindowSize * 2)
                            isCycleFound = true;
                    }
                    else
                    {
                        for (slidingWindowSize = fallenRocksRecording.Count - 1; slidingWindowSize > 1; slidingWindowSize--)
                            if (fallenRocksRecording[^1].RockPosition == fallenRocksRecording[^(1 + slidingWindowSize)].RockPosition)
                                break;
                        cycleStart = fallenRocksRecording.Count - slidingWindowSize;
                    }
                }
                // Console.WriteLine($"{countOfFallenRocks} key={key} top={highestPoint} starting {start} dist = {dist}.");
            }
            long numberOfExpectedFallenRocks = 1000000000000;
            numberOfExpectedFallenRocks -= 1; // we are counting from zero
            var notCompletedCycle = (int)((numberOfExpectedFallenRocks - cycleStart) % slidingWindowSize);
            var heightAtCycleStart = fallenRocksRecording[cycleStart + notCompletedCycle].Height;
            var numberOfCycles = (numberOfExpectedFallenRocks - cycleStart) / slidingWindowSize;
            var heightOfACycle = fallenRocksRecording[cycleStart + slidingWindowSize].Height - fallenRocksRecording[cycleStart].Height;
            yield return $"{numberOfCycles} X {heightOfACycle} + {heightAtCycleStart} = {numberOfCycles * heightOfACycle + heightAtCycleStart}";
        }
    }
}