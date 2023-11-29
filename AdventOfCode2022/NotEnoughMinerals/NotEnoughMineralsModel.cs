using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.NotEnoughMinerals
{
    public class NotEnoughMineralsModel : IPuzzleModel
    {
        private List<BluePrintData>? _bluePrints;
        public List<BluePrintData>? BluePrints => _bluePrints ;

        public void Parse(string puzzleInput)
        {
            var regex = new Regex(@"Blueprint (\d+): Each ore robot costs (\d+) ore. Each clay robot costs (\d+) ore. Each obsidian robot costs (\d+) ore and (\d+) clay. Each geode robot costs (\d+) ore and (\d+) obsidian.");
            _bluePrints = puzzleInput.Split("\n")
                .Select(x => regex.Match(x).Groups.Values.Skip(1).Select(x => int.Parse(x.Value)).ToArray())
                .Select(x => new BluePrintData
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

        public static (int MaxGeodes, int IterationsDone) MaxGeodesPossible(BluePrintData bluePrint, int maxMinutes)
        {
            var stack = new Stack<FactoryData>();
            stack.Push(FirstRobot(RobotType.ClayRobot));
            stack.Push(FirstRobot(RobotType.OreRobot));
            var bestScore = 0;
            var iterationsDone = 0;
            while (stack.TryPop(out var currentFactoryData))
            {
                iterationsDone++;
                var timeRemaining = maxMinutes - currentFactoryData.Minutes;
                if (bestScore >= MaxGeodesEstimated(currentFactoryData, timeRemaining))
                    continue;
                while (currentFactoryData.Minutes < maxMinutes && HasEnoughMineralsToBuildRobot(bluePrint, currentFactoryData))
                    CollectMinerals(ref currentFactoryData);
                CollectMinerals(ref currentFactoryData);
                if (currentFactoryData.Minutes > maxMinutes)
                {
                    if (currentFactoryData.Geodes > bestScore)
                        bestScore = currentFactoryData.Geodes;
                    continue;
                }
                TargetRobotIsNowBuilt(bluePrint, ref currentFactoryData);
                foreach (var FactoryData in TargetNewRobots(currentFactoryData))
                    stack.Push(FactoryData);
            }
            return (bestScore, iterationsDone);
        }

        public static int MaxGeodesEstimated(FactoryData f, int timeRemaining)
        {
            var maxNewGeodeRobots = timeRemaining;
            if (f.RobotToBuild != RobotType.GeodeRobot)
                maxNewGeodeRobots--;
            if (f.ObsidianRobots == 0)
                maxNewGeodeRobots--;
            if (f.ClayRobots == 0)
                maxNewGeodeRobots--;
            if (maxNewGeodeRobots < 0)
                maxNewGeodeRobots = 0;
            var geodesCollectedByNewRobots = maxNewGeodeRobots * (maxNewGeodeRobots + 1) / 2;
            var maxGeodesPossible = f.Geodes + f.GeodeRobots * (timeRemaining + 1) + geodesCollectedByNewRobots;
            return maxGeodesPossible;
        }

        public static FactoryData FirstRobot(RobotType robotType)
        {
            return new FactoryData
            {
                Minutes = 1,
                RobotToBuild = robotType,
                OreRobots = 1,
            };
        }

        public static IEnumerable<FactoryData> TargetNewRobots(FactoryData currentFactoryData)
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

        public static void CollectMinerals(ref FactoryData factory)
        {
            factory.Minutes++;
            factory.Ores += factory.OreRobots;
            factory.Clays += factory.ClayRobots;
            factory.Obsidians += factory.ObsidianRobots;
            factory.Geodes += factory.GeodeRobots;
        }

        public static bool HasEnoughMineralsToBuildRobot(BluePrintData bp, FactoryData factory)
        {
            var cost = bp.CostOfRobots![factory.RobotToBuild];
            return factory.Ores < cost.Ores || factory.Clays < cost.Clays || factory.Obsidians < cost.Obsidians;
        }

        public static void TargetRobotIsNowBuilt(BluePrintData bp, ref FactoryData factory)
        {
            var (ores, clays, obsidians) = bp.CostOfRobots![factory.RobotToBuild];
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
