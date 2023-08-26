using System.Text.RegularExpressions;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(19, "Not Enough Minerals")]
    public class NotEnoughMinerals : IPuzzleSolution
    {
        private string _puzzleInput = string.Empty;
        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }
        struct BluePrint
        {
            public int BlueprintNumber;
            public IReadOnlyDictionary<RobotTypes, (int Ores, int Clays, int Obsidians)> CostOfRobots;
        }

        public enum RobotTypes { OreRobot, ClayRobot, ObsidianRobot, GeodeRobot }

        public string SolveFirstPart()
        {
            var bluePrints = LoadBluePrints(_puzzleInput);
            var quality = 0;
            var maxMinutes = 24;
            foreach (var bp in bluePrints)
            {
                var maxGeodes = ComputeMaxGeodes(bp, maxMinutes);
                quality += maxGeodes * bp.BlueprintNumber;
            }
            return $"{quality}";
        }

        public string SolveSecondPart()
        {
            var blueprints = LoadBluePrints(_puzzleInput);
            var quality = 1;
            foreach (var bp in blueprints.Take(3))
            {
                var maxGeodes = ComputeMaxGeodes(bp, 32);
                Console.WriteLine($"{bp.BlueprintNumber} Score = {maxGeodes}");
                quality *= maxGeodes;
            }
            return $"{quality}";
        }

        private static List<BluePrint> LoadBluePrints(string puzzleInput)
        {
            var regex = new Regex(@"Blueprint (\d+): Each ore robot costs (\d+) ore. Each clay robot costs (\d+) ore. Each obsidian robot costs (\d+) ore and (\d+) clay. Each geode robot costs (\d+) ore and (\d+) obsidian.");
            return puzzleInput.Split("\n")
                .Select(x => regex.Match(x).Groups.Values.Skip(1).Select(x => int.Parse(x.Value)).ToArray())
                .Select(x => new BluePrint
                {
                    BlueprintNumber = x[0],
                    CostOfRobots = new Dictionary<RobotTypes, (int Ore, int Clay, int Obsidian)>()
                    {
                        {RobotTypes.OreRobot, (x[1], 0, 0) },
                        {RobotTypes.ClayRobot, (x[2], 0, 0) },
                        {RobotTypes.ObsidianRobot, (x[3],x[4], 0) },
                        {RobotTypes.GeodeRobot, (x[5], 0, x[6]) }
                    }
                })
                .ToList();
        }

        private static int ComputeMaxGeodes(BluePrint bluePrint, int maxMinutes)
        {
            var stack = new Stack<FactoryState>();
            stack.Push(FirstRobot(bluePrint, RobotTypes.OreRobot));
            stack.Push(FirstRobot(bluePrint, RobotTypes.ClayRobot));
            var bestScore = 0;
            while (stack.TryPop(out var currentFactoryState))
            {
                var timeRemaining = maxMinutes - currentFactoryState.Minutes + 1;
                var sumOfSecondsFromOneToTimeRemaining = timeRemaining * (timeRemaining - 1) / 2;
                var maxGeodesPossible = currentFactoryState.Geodes + currentFactoryState.GeodeRobots * timeRemaining + sumOfSecondsFromOneToTimeRemaining;
                if (maxGeodesPossible < bestScore)
                    continue;
                while (currentFactoryState.Minutes < maxMinutes && currentFactoryState.HasNotEnoughMineralsToBuildTheRobot())
                    currentFactoryState.CollectMinerals();
                currentFactoryState.CollectMinerals();
                if (currentFactoryState.Minutes == maxMinutes + 1)
                {
                    if (currentFactoryState.Geodes > bestScore)
                        bestScore = currentFactoryState.Geodes;
                    continue;
                }
                currentFactoryState.TargetRobotIsNowBuilt();
                foreach (var factoryState in TargetNewRobots(currentFactoryState))
                    stack.Push(factoryState);
            }
            return bestScore;
        }

        private static FactoryState FirstRobot(BluePrint bluePrint, RobotTypes robotType)
        {
            return new FactoryState
            {
                Minutes = 1,
                RobotToBuild = robotType,
                OreRobots = 1,
                CostOfRobots = bluePrint.CostOfRobots
            };
        }

        private static IEnumerable<FactoryState> TargetNewRobots(FactoryState currentFactoryState)
        {
            currentFactoryState.RobotToBuild = RobotTypes.OreRobot;
            yield return currentFactoryState;
            currentFactoryState.RobotToBuild = RobotTypes.ClayRobot;
            yield return currentFactoryState;
            currentFactoryState.RobotToBuild = RobotTypes.ObsidianRobot;
            if (currentFactoryState.ClayRobots > 0)
                yield return currentFactoryState;
            currentFactoryState.RobotToBuild = RobotTypes.GeodeRobot;
            if (currentFactoryState.ObsidianRobots > 0)
                yield return currentFactoryState;
        }

        struct FactoryState
        {
            public RobotTypes RobotToBuild;
            public int Minutes;
            public int Ores;
            public int Clays;
            public int Obsidians;
            public int Geodes;
            public int OreRobots;
            public int ClayRobots;
            public int ObsidianRobots;
            public int GeodeRobots;
            public IReadOnlyDictionary<RobotTypes, (int Ores, int Clays, int Obsidians)> CostOfRobots;
            public void CollectMinerals()
            {
                Minutes++;
                Ores += OreRobots;
                Clays += ClayRobots;
                Obsidians += ObsidianRobots;
                Geodes += GeodeRobots;
            }
            public bool HasNotEnoughMineralsToBuildTheRobot()
            {
                var cost = CostOfRobots[RobotToBuild];
                return Ores < cost.Ores || Clays < cost.Clays || Obsidians < cost.Obsidians;
            }

            public void TargetRobotIsNowBuilt()
            {
                var cost = CostOfRobots[RobotToBuild];
                if (RobotToBuild == RobotTypes.OreRobot)
                    OreRobots++;
                if (RobotToBuild == RobotTypes.ClayRobot)
                    ClayRobots++;
                if (RobotToBuild == RobotTypes.ObsidianRobot)
                    ObsidianRobots++;
                if (RobotToBuild == RobotTypes.GeodeRobot)
                    GeodeRobots++;
                Ores -= cost.Ores;
                Clays -= cost.Clays;
                Obsidians -= cost.Obsidians;
            }
        }
    }
}
