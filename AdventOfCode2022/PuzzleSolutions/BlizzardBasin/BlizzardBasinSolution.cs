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
        private int _gridWidth;
        private int _gridHeight;
        private int _currentMinute;
        private (int X, int Y) _entrancePosition;
        private (int X, int Y) _exitPosition;
        private List<List<(int ParentId, (int X, int Y) Pos)>>? _tree = new();
        private List<((int X, int Y) Position, Directions Direction)>? _blizzardsInitialPosition;

        private static readonly List<(int dx, int dy)> AllowedMoves = new()
        {
            (0,0),
            (1,0),
            (-1,0),
            (0,-1),
            (0,1),
        };

        private HashSet<(int x, int y)>? Walls;
        private string[]? Input { get; set; }

        public void Initialize(string puzzleInput)
        {
            Input = puzzleInput.Split("\n");
            Reset();
        }

        private void Reset()
        {
            ResetSimulationTime();
            GetGridDimensions();
            GetEntranceAndExitPositions();
            GetWallPositions();
            GetBlizzardsPositions();
            InitializeBFSSearchTree();
        }

        private void ResetSimulationTime()
        {
            _currentMinute = 0;
        }

        private void InitializeBFSSearchTree()
        {
            _tree.Clear();
            _tree.Add(new List<(int ParentId, (int X, int Y) Pos)>() { (0, _entrancePosition) });
        }

        private void GetBlizzardsPositions()
        {
            _blizzardsInitialPosition = Input!
                .SelectMany((line, row) => line.Select((c, col) => (c, col, row))
                .Where(y => "><^v".Contains(y.c)))
                .Select(e => ((x: e.col, y: e.row), e.c == '>' ? Directions.Right : e.c == '<' ? Directions.Left : e.c == '^' ? Directions.Up : Directions.Down))
                .ToList();
        }

        private void GetWallPositions()
        {
            Walls = Input!
                .SelectMany((line, row) => line.Select((c, col) => (c, col, row))
                .Where(y => y.c == '#'))
                .Select(e => (x: e.col, y: e.row))
                .ToHashSet();
        }

        private void GetEntranceAndExitPositions()
        {
            _entrancePosition = (Input![0].IndexOf('.'), 0);
            _exitPosition = (Input[^1].IndexOf('.'), Input.Length - 1);
        }

        private void GetGridDimensions()
        {
            _gridWidth = Input![0].Length;
            _gridHeight = Input.Length;
        }

        private static int Mod(int x, int m) => (x % m + m) % m;

        private static int ManhattanDistance((int x, int y) a, (int x, int y) b) => Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);

        private IEnumerable<((int X, int Y) Position, Directions Direction)> GetBlizzardsPositionAtTime(int minute)
            => _blizzardsInitialPosition!
            .Select(b => (X: b.Position.X - 1, Y: b.Position.Y - 1, b.Direction))
            .Select(b => (X: b.X + minute * AllowedMoves[(int)b.Direction].dx, Y: b.Y + minute * AllowedMoves[(int)b.Direction].dy, b.Direction))
            .Select(b => (X: Mod(b.X, _gridWidth - 2), Y: Mod(b.Y, _gridHeight - 2), b.Direction))
            .Select(b => ((b.X + 1, b.Y + 1), b.Direction));

        private BlizzardBasinInfo UpdateInfo(BlizzardBasinInfo info)
        {
            info.GridWidth = _gridWidth;
            info.GridHeight = _gridHeight;
            info.CurrentMinute = _currentMinute;
            info.EntrancePosition = _entrancePosition;
            info.ExitPosition = _exitPosition;
            info.Tree = _tree;
            info.BlizzardsPositions = GetBlizzardsPositionAtTime(_currentMinute);
            return info;
        }
        public IEnumerable<PuzzleOutput> SolveFirstPart(string puzzleInput)
        {
            Initialize(puzzleInput);
            var output = new PuzzleOutputProvider();
            var info = new BlizzardBasinInfo();
            yield return output.Put("Starting",UpdateInfo(info));
            var treeLevels = GetTreeLevelsAndIncrementCurrentMinute(_entrancePosition, _exitPosition);
            foreach (var treeLevel in treeLevels)
            {
                _tree.Add(treeLevel);
                yield return output.Put($"{_currentMinute}", UpdateInfo(info));
            }
        }

        public IEnumerable<PuzzleOutput> SolveSecondPart(string puzzleInput)
        {
            Initialize(puzzleInput);
            var output = new PuzzleOutputProvider();
            var info = new BlizzardBasinInfo();
            yield return output.Put("Starting", UpdateInfo(info));
            var treeLevels = GetTreeLevelsAndIncrementCurrentMinute(_entrancePosition, _exitPosition)
                .Concat(GetTreeLevelsAndIncrementCurrentMinute(_exitPosition, _entrancePosition)
                .Concat(GetTreeLevelsAndIncrementCurrentMinute(_entrancePosition, _exitPosition)));
            foreach (var treeLevel in treeLevels)
            {
                _tree.Add(treeLevel);
                yield return output.Put($"{_currentMinute}", UpdateInfo(info));
            }
        }

        private IEnumerable<List<(int ParentId, (int X, int Y) Pos)>> GetTreeLevelsAndIncrementCurrentMinute((int X, int Y) start, (int X, int Y) end)
        {
            var currentTreeLevel = CreateTreeLevelWithSingleNode(start, 0);
            do
            {
                IncrementCurrentMinute();
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
                var boxesOccupiedByBlizzards = GetBoxesOccupiedByBlizzards();
                bool isSolutionFound = false;
                var nextTreeLevel = NewEmptyTreeLevel();
                for (var parentId = 0; parentId < currentTreeLevel.Count && !isSolutionFound; parentId++)
                {
                    var (x, y) = currentTreeLevel[parentId].Pos;
                    foreach (var move in AllowedMoves)
                    {
                        var box = (X: x + move.dx, Y: y + move.dy);
                        if (IsBoxOutOfGrid(box))
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
                return blizzardsPosition.Contains(child) || Walls!.Contains(child);
            }

            bool IsBoxOutOfGrid((int X, int Y) child)
            {
                return child.Y < 0 || child.Y >= _gridHeight;
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

            HashSet<(int X, int Y)> GetBoxesOccupiedByBlizzards()
            {
                return GetBlizzardsPositionAtTime(_currentMinute).Select(b => (b.Position.X, b.Position.Y)).ToHashSet();
            }
        }

        private void IncrementCurrentMinute()
        {
            _currentMinute++;
        }
    }
}

