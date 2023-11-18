namespace sylvain69780.AdventOfCode2022.Domain.BlizzardBasin
{
    public class BlizzardBasinInfo
    {
        public List<List<(int ParentId, (int X, int Y) Pos)>> Tree { get; set; } = new ();
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }
        public int CurrentMinute { get; set; }
        public (int X, int Y) EntrancePosition { get; set; }
        public (int X, int Y) ExitPosition { get; set; }
        public IEnumerable<((int X, int Y) Position, Directions Direction)>? BlizzardsPositions { get; set; }
    }
}
