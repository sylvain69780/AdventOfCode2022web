﻿namespace AdventOfCode2022web.Puzzles
{
    public interface IBlizzardBasinViewState
    {
        int GridWidth { get; }
        int GridHeight { get; }
        int CurrentMinute { get; }
        (int X, int Y) EntrancePosition { get; }
        (int X, int Y) ExitPosition { get; }
        IEnumerable<((int X, int Y) Position, Directions Direction)>? BlizzardsPositions { get; }
        List<List<(int ParentId, (int X, int Y) Pos)>> Tree { get; }
    }

    public enum Directions
    {
        Halt = 0,
        Right = 1,
        Left = 2,
        Up = 3,
        Down = 4
    }

    [Puzzle(24, "Blizzard Basin", true)]
    public class BlizzardBasin : IPuzzleSolutionIter, IBlizzardBasinViewState
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
            var queue = new List<(int ParentId, (int X, int Y) Pos)>
            {
                (0, start)
            };
            var found = false;
            do
            {
                IncrementCurrentMinute();
                var blizzardsPosition = GetBlizzardsPositionAtTime(CurrentMinute).Select(b => (b.Position.X, b.Position.Y)).ToHashSet();
                var newQueue = new List<(int ParentId, (int X, int Y) Pos)>();
                for (var parentId = 0; parentId < queue.Count && !found; parentId++)
                {
                    var (x, y) = queue[parentId].Pos;
                    foreach (var move in AllowedMoves)
                    {
                        var box = (X: x + move.dx, Y: y + move.dy);
                        if (IsOutOfGrid(box))
                            continue;
                        if (IsBlockedByBlizzardsOrWalls(box, blizzardsPosition))
                            continue;
                        if (!IsStayInPlace(move) && IsBoxAlreadyInQueue(box, queue))
                            continue;
                        if (IsBoxAlreadyInQueue(box, newQueue))
                            continue;
                        newQueue.Add((parentId, box));
                        if (box == end)
                        {
                            found = true;
                            break;
                        }
                    }
                }
                queue = newQueue;
                if (found)
                    KeepOnlyFinalNode(end, ref queue);
                yield return queue;
            } while (!found && queue.Count > 0);
            if (queue.Count == 0)
                throw new InvalidDataException("No solution found");

            bool IsBlockedByBlizzardsOrWalls((int X, int Y) child,HashSet< (int X, int Y)> blizzardsPosition)
            {
                return blizzardsPosition.Contains(child) || Walls!.Contains(child);
            }

            bool IsOutOfGrid((int X, int Y) child)
            {
                return child.Y < 0 || child.Y >= GridHeight;
            }

            static bool IsBoxAlreadyInQueue((int X, int Y) box,List<(int ParentId, (int X, int Y) Pos)> queue)
            {
                return queue.Any(x => x.Pos == box);
            }

            static bool IsStayInPlace((int dx, int dy) move)
            {
                return move == (0,0);
            }

            static void KeepOnlyFinalNode((int X, int Y) box, ref List<(int ParentId, (int X, int Y) Pos)> queue)
            {
                queue = queue.Where(x => x.Pos == box).ToList();
            }
        }

        private void IncrementCurrentMinute()
        {
            CurrentMinute++;
        }
    }
}

