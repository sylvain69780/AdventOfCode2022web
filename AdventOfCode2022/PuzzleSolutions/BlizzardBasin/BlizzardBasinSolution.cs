using AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting;

namespace AdventOfCode2022Solutions.PuzzleSolutions.BlizzardBasin
{
    public enum Directions
    {
        Halt = 0,
        Right = 1,
        Left = 2,
        Up = 3,
        Down = 4
    }

    public class BlizzardBasinSolution : IPuzzleSolver
    {
        private List<((int X, int Y) Position, Directions Direction)>? _blizzardsInitialPosition;
        private static readonly List<(int dx, int dy)> AllowedMoves = new()
        {
            (0,0),
            (1,0),
            (-1,0),
            (0,-1),
            (0,1),
        };

        private HashSet<(int x, int y)>? _walls;
        private string[]? _input { get; set; }

        public void Initialize(BlizzardBasinInfo info,string puzzleInput)
        {
            _input = puzzleInput.Split("\n");
            Reset(info);
        }

        private void Reset(BlizzardBasinInfo info)
        {
            ResetSimulationTime(info);
            GetGridDimensions(info);
            GetEntranceAndExitPositions(info);
            GetWallPositions();
            GetBlizzardsPositions();
            InitializeBFSSearchTree(info);
        }

        private void ResetSimulationTime(BlizzardBasinInfo info)
        {
            info.CurrentMinute = 0;
        }

        private void InitializeBFSSearchTree(BlizzardBasinInfo info)
        {
            info.Tree!.Clear();
            info.Tree.Add(new List<(int ParentId, (int X, int Y) Pos)>() { (0, info.EntrancePosition) });
        }

        private void GetBlizzardsPositions()
        {
            _blizzardsInitialPosition = _input!
                .SelectMany((line, row) => line.Select((c, col) => (c, col, row))
                .Where(y => "><^v".Contains(y.c)))
                .Select(e => ((x: e.col, y: e.row), e.c == '>' ? Directions.Right : e.c == '<' ? Directions.Left : e.c == '^' ? Directions.Up : Directions.Down))
                .ToList();
        }

        private void GetWallPositions()
        {
            _walls = _input!
                .SelectMany((line, row) => line.Select((c, col) => (c, col, row))
                .Where(y => y.c == '#'))
                .Select(e => (x: e.col, y: e.row))
                .ToHashSet();
        }

        private void GetEntranceAndExitPositions(BlizzardBasinInfo info)
        {
            info.EntrancePosition = (_input![0].IndexOf('.'), 0);
            info.ExitPosition = (_input[^1].IndexOf('.'), _input.Length - 1);
        }

        private void GetGridDimensions(BlizzardBasinInfo info)
        {
            info.GridWidth = _input![0].Length;
            info.GridHeight = _input.Length;
        }

        private static int Mod(int x, int m) => (x % m + m) % m;

        private static int ManhattanDistance((int x, int y) a, (int x, int y) b) => Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);

        private IEnumerable<((int X, int Y) Position, Directions Direction)> GetBlizzardsPositionAtTime(BlizzardBasinInfo info,int minute)
            => _blizzardsInitialPosition!
            .Select(b => (X: b.Position.X - 1, Y: b.Position.Y - 1, b.Direction))
            .Select(b => (X: b.X + minute * AllowedMoves[(int)b.Direction].dx, Y: b.Y + minute * AllowedMoves[(int)b.Direction].dy, b.Direction))
            .Select(b => (X: Mod(b.X, info.GridWidth - 2), Y: Mod(b.Y, info.GridHeight - 2), b.Direction))
            .Select(b => ((b.X + 1, b.Y + 1), b.Direction));

        private BlizzardBasinInfo UpdateInfo(BlizzardBasinInfo info)
        {
            info.BlizzardsPositions = GetBlizzardsPositionAtTime(info,info.CurrentMinute);
            return info;
        }
        public IEnumerable<PuzzleOutput> SolveFirstPart(string puzzleInput)
        {
            var output = new PuzzleOutputProvider();
            var info = new BlizzardBasinInfo();
            Initialize(info, puzzleInput);
            yield return output.Put("Starting",UpdateInfo(info));
            var treeLevels = GetTreeLevelsAndIncrementCurrentMinute(info,info.EntrancePosition, info.ExitPosition);
            foreach (var treeLevel in treeLevels)
            {
                info.Tree.Add(treeLevel);
                yield return output.Put($"{info.CurrentMinute}", UpdateInfo(info));
            }
        }

        public IEnumerable<PuzzleOutput> SolveSecondPart(string puzzleInput)
        {
            var output = new PuzzleOutputProvider();
            var info = new BlizzardBasinInfo();
            Initialize(info,puzzleInput);
            yield return output.Put("Starting", UpdateInfo(info));
            var treeLevels = GetTreeLevelsAndIncrementCurrentMinute(info,info.EntrancePosition, info.ExitPosition)
                .Concat(GetTreeLevelsAndIncrementCurrentMinute(info,info.ExitPosition, info.EntrancePosition)
                .Concat(GetTreeLevelsAndIncrementCurrentMinute(info,info.EntrancePosition, info.ExitPosition)));
            foreach (var treeLevel in treeLevels)
            {
                info.Tree.Add(treeLevel);
                yield return output.Put($"{info.CurrentMinute}", UpdateInfo(info));
            }
        }

        private IEnumerable<List<(int ParentId, (int X, int Y) Pos)>> GetTreeLevelsAndIncrementCurrentMinute(BlizzardBasinInfo info, (int X, int Y) start, (int X, int Y) end)
        {
            var currentTreeLevel = CreateTreeLevelWithSingleNode(start, 0);
            do
            {
                IncrementCurrentMinute(info);
                var searchResult = GetNextTreeLevel(end, currentTreeLevel);
                currentTreeLevel = searchResult.NextTreeLevel;
                yield return currentTreeLevel;
                if (searchResult.IsDestinationFound)
                    break;
            } while (currentTreeLevel.Count > 0);
            if (currentTreeLevel.Count == 0)
                throw new InvalidDataException("No solution found");

            (bool IsDestinationFound, List<(int ParentId, (int X, int Y) Pos)> NextTreeLevel) GetNextTreeLevel((int X, int Y) end, List<(int ParentId, (int X, int Y) Pos)> currentTreeLevel)
            {
                var boxesOccupiedByBlizzards = GetBoxesOccupiedByBlizzards(info);
                bool isSolutionFound = false;
                var nextTreeLevel = NewEmptyTreeLevel();
                for (var parentId = 0; parentId < currentTreeLevel.Count && !isSolutionFound; parentId++)
                {
                    var (x, y) = currentTreeLevel[parentId].Pos;
                    foreach (var move in AllowedMoves)
                    {
                        var box = (X: x + move.dx, Y: y + move.dy);
                        if (IsBoxOutOfGrid(info,box))
                            continue;
                        if (IsBoxBlockedByBlizzardsOrWalls(box, boxesOccupiedByBlizzards))
                            continue;
                        if (IsDisplacement(move) && IsBoxAlreadyInTreeLevel(box, currentTreeLevel))
                            continue;
                        if (IsBoxAlreadyInTreeLevel(box, nextTreeLevel))
                            continue;
                        if (box == end)
                        {
                            nextTreeLevel = CreateTreeLevelWithSingleNode(box, parentId);
                            isSolutionFound = true;
                            break;
                        }
                        nextTreeLevel.Add((parentId, box));
                    }
                }
                return (isSolutionFound, nextTreeLevel.OrderBy(x => ManhattanDistance(x.Pos, end)).Take(100).ToList());
            }

            bool IsBoxBlockedByBlizzardsOrWalls((int X, int Y) child, HashSet<(int X, int Y)> blizzardsPosition)
            {
                return blizzardsPosition.Contains(child) || _walls!.Contains(child);
            }

            bool IsBoxOutOfGrid(BlizzardBasinInfo info, (int X, int Y) child)
            {
                return child.Y < 0 || child.Y >= info.GridHeight;
            }

            static bool IsBoxAlreadyInTreeLevel((int X, int Y) box, List<(int ParentId, (int X, int Y) Pos)> queue)
            {
                return queue.Any(x => x.Pos == box);
            }

            static bool IsDisplacement((int dx, int dy) move)
            {
                return move != (0, 0);
            }

            static List<(int ParentId, (int X, int Y) Pos)> CreateTreeLevelWithSingleNode((int X, int Y) box, int parentId)
            {
                return new List<(int ParentId, (int X, int Y) Pos)>
            {
                (parentId, box)
            };
            }

            static List<(int ParentId, (int X, int Y) Pos)> NewEmptyTreeLevel()
            {
                return new List<(int ParentId, (int X, int Y) Pos)>();
            }

            HashSet<(int X, int Y)> GetBoxesOccupiedByBlizzards(BlizzardBasinInfo info)
            {
                return GetBlizzardsPositionAtTime(info,info.CurrentMinute).Select(b => (b.Position.X, b.Position.Y)).ToHashSet();
            }
        }

        private void IncrementCurrentMinute(BlizzardBasinInfo info)
        {
            info.CurrentMinute++;
        }
    }
}

