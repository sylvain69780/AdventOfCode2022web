using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PyroclasticFlow
{
    public class PyroclasticFlowPart2Strategy : IPuzzleStrategy<PyroclasticFlowModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(PyroclasticFlowModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var jetGenerator = new JetGenerator { JetPattern = model.PuzzleInput };
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
            yield return updateContext();
            //provideSolution($"{numberOfCycles} X {heightOfACycle} + {heightAtCycleStart} = {numberOfCycles * heightOfACycle + heightAtCycleStart}");
            provideSolution($"{numberOfCycles * heightOfACycle + heightAtCycleStart}");
        }
    }
}
