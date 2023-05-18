using System.Text.RegularExpressions;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(19, "Not Enough Minerals")]
    public class NotEnoughMinerals : IPuzzleSolver
    {

        private static List<BluePrint> GetBluePrints(string puzzleInput)
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

        public string SolveFirstPart(string puzzleInput)
        {
            var bluePrints = GetBluePrints(puzzleInput);
            var quality = 0;
            var maxMinutes = 24;
            foreach (var bp in bluePrints)
            {
                var maxGeodes = bp.ComputeMaxGeodes(maxMinutes);
                quality += maxGeodes * bp.BlueprintNumber;
            }
            return $"{quality}";
        }
        public string SolveSecondPart(string puzzleInput)
        {
            var blueprints = GetBluePrints(puzzleInput);
            var quality = 1;
            foreach (var bp in blueprints.Take(3))
            {
                var maxGeodes = bp.ComputeMaxGeodes(32);
                Console.WriteLine($"{bp.BlueprintNumber} Score = {maxGeodes}");
                quality *= maxGeodes;
            }
            return $"{quality}";
        }

        struct BluePrint
        {
            public int BlueprintNumber;
            public IReadOnlyDictionary<RobotTypes, (int Ores, int Clays, int Obsidians)> CostOfRobots;
            public int ComputeMaxGeodes(int maxMinutes)
            {
                var factoryState = new FactoryState
                {
                    Minutes = 1,
                    RobotToBuild = RobotTypes.OreRobot,
                    OreRobots = 1,
                    CostOfRobots = CostOfRobots
                };

                var stack = new Stack<FactoryState>();
                stack.Push(factoryState);
                factoryState.RobotToBuild = RobotTypes.ClayRobot;
                stack.Push(factoryState);
                var bestScore = 0;
                while (stack.TryPop(out var currentFactoryState))
                {
                    var timeRemaining = maxMinutes - currentFactoryState.Minutes + 1;
                    var sumOfSecondsFromOneToTimeRemaining = timeRemaining * (timeRemaining - 1) / 2;
                    var maxGeodesPossible = currentFactoryState.Geodes + currentFactoryState.GeodeRobots * timeRemaining + sumOfSecondsFromOneToTimeRemaining;
                    if (maxGeodesPossible < bestScore)
                        continue;
                    while (currentFactoryState.Minutes < maxMinutes && currentFactoryState.HasNotEnoughMinerals())
                        currentFactoryState.CollectMinerals();
                    currentFactoryState.CollectMinerals();
                    if (currentFactoryState.Minutes == maxMinutes + 1)
                    {
                        if (currentFactoryState.Geodes > bestScore)
                            bestScore = currentFactoryState.Geodes;
                        continue;
                    }
                    currentFactoryState.BuildRobot();
                    currentFactoryState.RobotToBuild = RobotTypes.OreRobot;
                    stack.Push(currentFactoryState);
                    currentFactoryState.RobotToBuild = RobotTypes.ClayRobot;
                    stack.Push(currentFactoryState);
                    currentFactoryState.RobotToBuild = RobotTypes.ObsidianRobot;
                    if (currentFactoryState.ClayRobots > 0)
                        stack.Push(currentFactoryState);
                    currentFactoryState.RobotToBuild = RobotTypes.GeodeRobot;
                    if (currentFactoryState.ObsidianRobots > 0)
                        stack.Push(currentFactoryState);
                }
                return bestScore;
            }
        }
        public enum RobotTypes
        {
            OreRobot, ClayRobot, ObsidianRobot, GeodeRobot
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
            public bool HasNotEnoughMinerals()
            {
                var cost = CostOfRobots[RobotToBuild];
                return Ores < cost.Ores || Clays < cost.Clays || Obsidians < cost.Obsidians;
            }

            public void BuildRobot()
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
