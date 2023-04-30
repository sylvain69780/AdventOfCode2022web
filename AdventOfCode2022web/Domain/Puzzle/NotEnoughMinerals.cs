using System.Text.RegularExpressions;

namespace AdventOfCode2022web.Domain.Puzzle
{
    public class NotEnoughMinerals : IPuzzleSolver
    {
        public IEnumerable<string> SolveFirstPart(string inp)
        {
            var r = new Regex(@"Blueprint (\d+): Each ore robot costs (\d+) ore. Each clay robot costs (\d+) ore. Each obsidian robot costs (\d+) ore and (\d+) clay. Each geode robot costs (\d+) ore and (\d+) obsidian.");
            var input = inp.Split("\n")
                .Select(x => r.Match(x).Groups.Values.Skip(1).Select(x => int.Parse(x.Value)).ToArray())
                .Select(x => new BluePrint
                {
                    Num = x[0],
                    OreRobotCost = x[1],
                    ClayRobotCost = x[2],
                    ObsidianRobotCost = (x[3], x[4]),
                    GeodeRobotCost = (x[5], x[6])
                })
                .ToList();
            var quality = 0;
            var maxMinutes = 24;
            foreach (var bp in input)
            {
                var score = bp.ComputeMaxGeodes(maxMinutes);
                quality += score * bp.Num;
            }
            yield return $"{quality}";
        }
        public IEnumerable<string> SolveSecondPart(string inp)
        {
            var r = new Regex(@"Blueprint (\d+): Each ore robot costs (\d+) ore. Each clay robot costs (\d+) ore. Each obsidian robot costs (\d+) ore and (\d+) clay. Each geode robot costs (\d+) ore and (\d+) obsidian.");
            var input = inp.Split("\n")
                .Select(x => r.Match(x).Groups.Values.Skip(1).Select(x => int.Parse(x.Value)).ToArray())
                .Select(x => new BluePrint
                {
                    Num = x[0],
                    OreRobotCost = x[1],
                    ClayRobotCost = x[2],
                    ObsidianRobotCost = (x[3], x[4]),
                    GeodeRobotCost = (x[5], x[6])
                })
                .ToList();
            var quality = 1;
            foreach (var bp in input.Take(3))
            {
                var score = bp.ComputeMaxGeodes(32);
                Console.WriteLine($"{bp.Num} Score = {score}");
                quality *= score;
            }
            yield return $"{quality}";
        }
        struct BluePrint
        {
            public int Num;
            public int OreRobotCost;
            public int ClayRobotCost;
            public (int ore, int clay) ObsidianRobotCost;
            public (int ore, int obsidian) GeodeRobotCost;
            public int ComputeMaxGeodes(int maxMinutes)
            {
                var factory = new FactoryState
                {
                    Minutes = 1,
                    RobotToBuild = RobotTypes.OreRobot,
                    OreRobots = 1
                };

                var search = new Stack<FactoryState>();
                factory.BackTrack = "1 - OreRobot";
                search.Push(factory);
                factory.RobotToBuild = RobotTypes.ClayRobot;
                factory.BackTrack = "1 - ClayRobot";
                search.Push(factory);
                var bestScore = 0;
                while (search.TryPop(out var state))
                {
                    var timeRemaining = maxMinutes - state.Minutes + 1;
                    var maxGeodesPossible = state.Geodes + state.GeodeRobots * timeRemaining + timeRemaining * (timeRemaining - 1) / 2;
                    if (maxGeodesPossible < bestScore)
                        continue;
                    if (state.RobotToBuild == RobotTypes.ObsidianRobot && (state.ClayRobots == 0))
                        continue;
                    if (state.RobotToBuild == RobotTypes.GeodeRobot && (state.ObsidianRobots == 0))
                        continue;
                    (int o, int c, int ob) cost = state.RobotToBuild == RobotTypes.OreRobot ? (OreRobotCost, 0, 0) :
                        state.RobotToBuild == RobotTypes.ClayRobot ? (ClayRobotCost, 0, 0) :
                        state.RobotToBuild == RobotTypes.ObsidianRobot ? (ObsidianRobotCost.ore, ObsidianRobotCost.clay, 0) :
                        (GeodeRobotCost.ore, 0, GeodeRobotCost.obsidian);
                    while ((state.Ores < cost.o || state.Clays < cost.c || state.Obsidians < cost.ob) && state.Minutes < maxMinutes)
                    {
                        state.Minutes++;
                        state.Ores += state.OreRobots;
                        state.Clays += state.ClayRobots;
                        state.Obsidians += state.ObsidianRobots;
                        state.Geodes += state.GeodeRobots;
                    }
                    {
                        state.Minutes++;
                        state.Ores += state.OreRobots;
                        state.Clays += state.ClayRobots;
                        state.Obsidians += state.ObsidianRobots;
                        state.Geodes += state.GeodeRobots;
                    }
                    if (state.Minutes == maxMinutes + 1)
                    {
                        if (state.Geodes > bestScore)
                        {
                            //Console.WriteLine($"{Num} {state.Ores} {state.Clays} {state.Obsidians} Geodes = {state.Geodes} ");
                            //Console.WriteLine(state.BackTrack);
                            bestScore = state.Geodes;
                        }
                        continue;
                    }
                    if (state.RobotToBuild == RobotTypes.OreRobot)
                    {
                        state.Ores -= OreRobotCost;
                        state.OreRobots++;
                    }
                    if (state.RobotToBuild == RobotTypes.ClayRobot)
                    {
                        state.Ores -= ClayRobotCost;
                        state.ClayRobots++;
                    }
                    if (state.RobotToBuild == RobotTypes.ObsidianRobot)
                    {
                        state.Ores -= ObsidianRobotCost.ore;
                        state.Clays -= ObsidianRobotCost.clay;
                        state.ObsidianRobots++;
                    }
                    if (state.RobotToBuild == RobotTypes.GeodeRobot)
                    {
                        state.Ores -= GeodeRobotCost.ore;
                        state.Obsidians -= GeodeRobotCost.obsidian;
                        state.GeodeRobots++;
                    }
                    var backTrack = state.BackTrack;
                    state.RobotToBuild = RobotTypes.OreRobot;
                    state.BackTrack = backTrack + $" - {state.Minutes} OreRobot";
                    search.Push((FactoryState)state);
                    state.RobotToBuild = RobotTypes.ClayRobot;
                    state.BackTrack = backTrack + $" - {state.Minutes} ClayRobot";
                    search.Push((FactoryState)state);
                    state.RobotToBuild = RobotTypes.ObsidianRobot;
                    state.BackTrack = backTrack + $" - {state.Minutes} ObsidianRobot";
                    search.Push((FactoryState)state);
                    state.RobotToBuild = RobotTypes.GeodeRobot;
                    state.BackTrack = backTrack + $" - {state.Minutes} GeodeRobot";
                    search.Push((FactoryState)state);
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
            public string BackTrack;
        }
    }
}
