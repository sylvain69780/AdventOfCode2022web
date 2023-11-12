using AdventOfCode2022Solutions.PuzzleSolutions.BlizzardBasin;

namespace AdventOfCode2022Solutions.PuzzleSolutions.BlizzardBasin
{
    public class BlizzardBasinOutputProvider
    {
        int _step = 0;
        public BlizzardBasinOutput Put(string output, int gridWidth, int gridHeight, int currentMinute, (int X, int Y) entrancePosition, (int X, int Y) exitPosition, List<List<(int ParentId, (int X, int Y) Pos)>>? tree, IEnumerable<((int X, int Y) Position, Directions Direction)>? blizzardsPositions)
        {
            return new BlizzardBasinOutput()
            {
                Step = _step++,
                Output = output,
                GridWidth = gridWidth,
                GridHeight = gridHeight,
                CurrentMinute = currentMinute,
                EntrancePosition = entrancePosition,
                ExitPosition = exitPosition,
                Tree = tree,
                BlizzardsPositions = blizzardsPositions,
            };
        }
    }
}
