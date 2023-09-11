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

    [Puzzle(24, "Blizzard Basin", true)]
    public class BlizzardBasinSolution : IPuzzleSolutionIter
    {
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }
        public int CurrentMinute { get; set; }
        public (int X, int Y) EntrancePosition { get; set; }
        public (int X, int Y) ExitPosition { get; set; }
        public List<List<(int ParentId, (int X, int Y) Pos)>> Tree { get; set; } = new();
        public IEnumerable<((int X, int Y) Position, Directions Direction)> BlizzardsPositions => GetBlizzardsPositionAtTime(CurrentMinute);

        private List<((int X, int Y) Position, Directions Direction)>? BlizzardsInitialPosition { get; set; }

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
            CurrentMinute = 0;
        }

        private void InitializeBFSSearchTree()
        {
            Tree.Clear();
            Tree.Add(new List<(int ParentId, (int X, int Y) Pos)>() { (0, EntrancePosition) });
        }

        private void GetBlizzardsPositions()
        {
            BlizzardsInitialPosition = Input!
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
            EntrancePosition = (Input![0].IndexOf('.'), 0);
            ExitPosition = (Input[^1].IndexOf('.'), Input.Length - 1);
        }

        private void GetGridDimensions()
        {
            GridWidth = Input![0].Length;
            GridHeight = Input.Length;
        }

        private static int Mod(int x, int m) => (x % m + m) % m;

        private static int ManhattanDistance((int x, int y) a, (int x, int y) b) => Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);

        private IEnumerable<((int X, int Y) Position, Directions Direction)> GetBlizzardsPositionAtTime(int minute)
            => BlizzardsInitialPosition!
            .Select(b => (X: b.Position.X - 1, Y: b.Position.Y - 1, b.Direction))
            .Select(b => (X: b.X + minute * AllowedMoves[(int)b.Direction].dx, Y: b.Y + minute * AllowedMoves[(int)b.Direction].dy, b.Direction))
            .Select(b => (X: Mod(b.X, GridWidth - 2), Y: Mod(b.Y, GridHeight - 2), b.Direction))
            .Select(b => ((b.X + 1, b.Y + 1), b.Direction));

        public IEnumerable<string> SolveFirstPart()
        {
            Reset();
            var treeLevels = GetTreeLevelsAndIncrementCurrentMinute(EntrancePosition, ExitPosition);
            foreach (var treeLevel in treeLevels)
            {
                Tree.Add(treeLevel);
                yield return $"{CurrentMinute}";
            }
        }

        public IEnumerable<string> SolveSecondPart()
        {
            Reset();
            var treeLevels = GetTreeLevelsAndIncrementCurrentMinute(EntrancePosition, ExitPosition)
                .Concat(GetTreeLevelsAndIncrementCurrentMinute(ExitPosition, EntrancePosition)
                .Concat(GetTreeLevelsAndIncrementCurrentMinute(EntrancePosition, ExitPosition)));
            foreach (var treeLevel in treeLevels)
            {
                Tree.Add(treeLevel);
                yield return $"{CurrentMinute}";
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
                return child.Y < 0 || child.Y >= GridHeight;
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
                return GetBlizzardsPositionAtTime(CurrentMinute).Select(b => (b.Position.X, b.Position.Y)).ToHashSet();
            }
        }

        private void IncrementCurrentMinute()
        {
            CurrentMinute++;
        }
    }
}

