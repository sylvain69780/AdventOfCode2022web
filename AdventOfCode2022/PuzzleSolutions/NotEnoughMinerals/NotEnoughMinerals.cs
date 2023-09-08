using System.Text.RegularExpressions;
using AdventOfCode2022web.Puzzles;

namespace AdventOfCode2022.PuzzleSolutions.NotEnoughMinerals
{
    public enum RobotType { OreRobot, ClayRobot, ObsidianRobot, GeodeRobot }

    public struct FactoryData
    {
        public RobotType RobotToBuild;
        public int Minutes;
        public int Ores;
        public int Clays;
        public int Obsidians;
        public int Geodes;
        public int OreRobots;
        public int ClayRobots;
        public int ObsidianRobots;
        public int GeodeRobots;
    }

    [Puzzle(19, "Not Enough Minerals")]
    public class NotEnoughMineralsSolution : IPuzzleSolutionIter
    {
        public List<List<FactoryData>> BestSolutions { get; } = new();

        private string _puzzleInput = string.Empty;

        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }
        struct BluePrint
        {
            public int BlueprintNumber;
            public IReadOnlyDictionary<RobotType, (int Ores, int Clays, int Obsidians)> CostOfRobots;
        }

        public IEnumerable<string> SolveFirstPart()
        {
            var bluePrints = LoadBluePrints(_puzzleInput);
            var quality = 0;
            var maxMinutes = 24;
            foreach (var bp in bluePrints)
            {
                var (maxGeodes,iterationsDone) = ComputeMaxGeodes(bp, maxMinutes);
                yield return $"Blueprint {bp.BlueprintNumber} gives at most {maxGeodes} geodes. {iterationsDone} iterations done.";
                quality += maxGeodes * bp.BlueprintNumber;
            }
            yield return $"{quality}";
        }

        public IEnumerable<string> SolveSecondPart()
        {
            var blueprints = LoadBluePrints(_puzzleInput);
            var quality = 1;
            foreach (var bp in blueprints.Take(3))
            {
                var (maxGeodes, iterationsDone) = ComputeMaxGeodes(bp, 32);
                yield return $"Blueprint {bp.BlueprintNumber} gives at most {maxGeodes} geodes. {iterationsDone} iterations done.";
                quality *= maxGeodes;
            }
            yield return $"{quality}";
        }

        private static List<BluePrint> LoadBluePrints(string puzzleInput)
        {
            var regex = new Regex(@"Blueprint (\d+): Each ore robot costs (\d+) ore. Each clay robot costs (\d+) ore. Each obsidian robot costs (\d+) ore and (\d+) clay. Each geode robot costs (\d+) ore and (\d+) obsidian.");
            return puzzleInput.Split("\n")
                .Select(x => regex.Match(x).Groups.Values.Skip(1).Select(x => int.Parse(x.Value)).ToArray())
                .Select(x => new BluePrint
                {
                    BlueprintNumber = x[0],
                    CostOfRobots = new Dictionary<RobotType, (int Ore, int Clay, int Obsidian)>()
                    {
                        {RobotType.OreRobot, (x[1], 0, 0) },
                        {RobotType.ClayRobot, (x[2], 0, 0) },
                        {RobotType.ObsidianRobot, (x[3],x[4], 0) },
                        {RobotType.GeodeRobot, (x[5], 0, x[6]) }
                    }
                })
                .ToList();
        }

        private static (int MaxGeodes,int IterationsDone) ComputeMaxGeodes(BluePrint bluePrint, int maxMinutes)
        {
            var stack = new Stack<FactoryData>();
            stack.Push(FirstRobot(RobotType.OreRobot));
            stack.Push(FirstRobot(RobotType.ClayRobot));
            var bestScore = 0;
                var iterationsDone = 0;
            while (stack.TryPop(out var currentFactoryData))
            {
                iterationsDone++;
                var timeRemaining = maxMinutes - currentFactoryData.Minutes + 1;
                var sumOfSecondsFromOneToTimeRemaining = timeRemaining * (timeRemaining - 1) / 2;
                var maxGeodesPossible = currentFactoryData.Geodes + currentFactoryData.GeodeRobots * timeRemaining + sumOfSecondsFromOneToTimeRemaining;
                if (maxGeodesPossible < bestScore)
                    continue;
                while (currentFactoryData.Minutes < maxMinutes && HasNotEnoughMineralsToBuildTheRobot(bluePrint,currentFactoryData))
                    CollectMinerals(ref currentFactoryData);
                CollectMinerals(ref currentFactoryData);
                if (currentFactoryData.Minutes == maxMinutes + 1)
                {
                    if (currentFactoryData.Geodes > bestScore)
                        bestScore = currentFactoryData.Geodes;
                    continue;
                }
                TargetRobotIsNowBuilt(bluePrint, ref currentFactoryData);
                foreach (var FactoryData in TargetNewRobots(currentFactoryData))
                    stack.Push(FactoryData);
            }
            return (bestScore,iterationsDone);
        }

        private static FactoryData FirstRobot(RobotType robotType)
        {
            return new FactoryData
            {
                Minutes = 1,
                RobotToBuild = robotType,
                OreRobots = 1,
            };
        }

        private static IEnumerable<FactoryData> TargetNewRobots(FactoryData currentFactoryData)
        {
            currentFactoryData.RobotToBuild = RobotType.OreRobot;
            yield return currentFactoryData;
            currentFactoryData.RobotToBuild = RobotType.ClayRobot;
            yield return currentFactoryData;
            currentFactoryData.RobotToBuild = RobotType.ObsidianRobot;
            if (currentFactoryData.ClayRobots > 0)
                yield return currentFactoryData;
            currentFactoryData.RobotToBuild = RobotType.GeodeRobot;
            if (currentFactoryData.ObsidianRobots > 0)
                yield return currentFactoryData;
        }

        private static void CollectMinerals(ref FactoryData factory)
        {
            factory.Minutes++;
            factory.Ores += factory.OreRobots;
            factory.Clays += factory.ClayRobots;
            factory.Obsidians += factory.ObsidianRobots;
            factory.Geodes += factory.GeodeRobots;
        }

        private static bool HasNotEnoughMineralsToBuildTheRobot(BluePrint bp, FactoryData factory)
        {
            var cost = bp.CostOfRobots[factory.RobotToBuild];
            return factory.Ores < cost.Ores || factory.Clays < cost.Clays || factory.Obsidians < cost.Obsidians;
        }

        private static void TargetRobotIsNowBuilt(BluePrint bp, ref FactoryData factory)
        {
            var (ores, clays, obsidians) = bp.CostOfRobots[factory.RobotToBuild];
            if (factory.RobotToBuild == RobotType.OreRobot)
                factory.OreRobots++;
            if (factory.RobotToBuild == RobotType.ClayRobot)
                factory.ClayRobots++;
            if (factory.RobotToBuild == RobotType.ObsidianRobot)
                factory.ObsidianRobots++;
            if (factory.RobotToBuild == RobotType.GeodeRobot)
                factory.GeodeRobots++;
            factory.Ores -= ores;
            factory.Clays -= clays;
            factory.Obsidians -= obsidians;
        }
    }
}
