namespace Domain.NotEnoughMinerals
{
    public class BluePrintData
    {
        public int BlueprintNumber;
        public IReadOnlyDictionary<RobotType, (int Ores, int Clays, int Obsidians)>? CostOfRobots;
    }
}
