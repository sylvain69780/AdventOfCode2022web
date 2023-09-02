using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AdventOfCode2022web.Puzzles
{
    public interface IBlizzardBasinViewModel
    {
        int GridWidth { get; }
        int GridHeight { get; }
        List<((int X, int Y) StartingPosition, (int X, int Y) TargetPosition, BlizzardBasinElfState State)>? Elves { get; }
        List<((int X, int Y) Position, Directions Direction)>? BlizzardsPositions { get; }
        (int X, int Y) EntrancePosition { get; }
        (int X, int Y) ExitPosition { get; }
    }

    public enum BlizzardBasinElfState
    {
        Possible,
        Safe,
        Killed
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
    public class BlizzardBasin : IPuzzleSolutionIter, IBlizzardBasinViewModel
    {
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }
        public List<((int X, int Y) StartingPosition, (int X, int Y) TargetPosition, BlizzardBasinElfState State)>? Elves { get; set; }
        public List<((int X, int Y) Position, Directions Direction)>? BlizzardsPositions { get; set; }
        public (int X, int Y) EntrancePosition { get; set; }
        public (int X, int Y) ExitPosition { get; set; }

        public Dictionary<(int x, int y, int t), (int x, int y, int t)>? Prev;
        public List<(int x, int y)>? DeadEnds { get; set; }

        private int RoundNumber { get; set; }

        private List<((int X, int Y) Position, Directions Direction)>? BlizzardsInitialPosition { get; set; }
        private IEnumerable<((int X, int Y) Position, Directions Direction)> BlizzardsPositionAtTime(int roundNumber)
            => BlizzardsInitialPosition!
            .Select(b => (X: b.Position.X - 1, Y: b.Position.Y - 1, b.Direction))
            .Select(b => (X: b.X + roundNumber * AllowedMoves[(int)b.Direction].dx, Y: b.Y + roundNumber * AllowedMoves[(int)b.Direction].dy, b.Direction))
            .Select(b => (X: Mod(b.X, GridWidth - 2), Y: Mod(b.Y, GridHeight - 2), b.Direction))
            .Select(b => ((b.X + 1, b.Y + 1), b.Direction));

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
            Walls = Input!
                .SelectMany((line, row) => line.Select((c, col) => (c, col, row))
                .Where(y => y.c == '#'))
                .Select(e => (x: e.col, y: e.row))
                .ToHashSet();
            BlizzardsInitialPosition = Input!
                .SelectMany((line, row) => line.Select((c, col) => (c, col, row))
                .Where(y => "><^v".Contains(y.c)))
                .Select(e => ((x: e.col, y: e.row), e.c == '>' ? Directions.Right : e.c == '<' ? Directions.Left : e.c == '^' ? Directions.Up : Directions.Down))
                .ToList();
            Prev = new Dictionary<(int x, int y, int t), (int x, int y, int t)>();
            DeadEnds = new List<(int x, int y)>();
            EntrancePosition = (Input![0].IndexOf('.'), 0);
            ExitPosition = (Input[^1].IndexOf('.'), Input.Length - 1);
            GridWidth = Input[0].Length;
            GridHeight = Input.Length;
            BlizzardsPositions = BlizzardsPositionAtTime(RoundNumber).ToList();
            Elves = new List<((int X, int Y) StartingPosition, (int X, int Y) TargetPosition, BlizzardBasinElfState State)>()
            {
                (StartingPosition:EntrancePosition,TargetPosition:EntrancePosition , BlizzardBasinElfState.Safe)
            };
        }

        private static int Mod(int x, int m) => (x % m + m) % m;

        public IEnumerable<string> SolveFirstPart()
        {
            Reset();
            RoundNumber = 0;
            List<List<(int ParentId, (int X, int Y) Pos)>> tree = new() { new List<(int ParentId, (int X, int Y) Pos)>() { (0, EntrancePosition) } };
            foreach (var treeLevel in GetTreeLevels(EntrancePosition, ExitPosition))
            {
                tree.Add(treeLevel);
                BlizzardsPositions = BlizzardsPositionAtTime(RoundNumber).ToList();
                Elves!.Clear();
                foreach (var child in tree[RoundNumber])
                {
                    var parent = tree[RoundNumber - 1][child.ParentId];
                    Elves.Add((StartingPosition: parent.Pos, TargetPosition: child.Pos, BlizzardBasinElfState.Possible));
                }
                var parentIds = tree[RoundNumber].Select(x => x.ParentId).ToHashSet();
                for (var parentId = 0; parentId < tree[RoundNumber-1].Count;parentId++)
                {
                    if (parentIds.Contains(parentId))
                        continue;
                    var parent = tree[RoundNumber - 1][parentId];
                    Elves.Add((StartingPosition: parent.Pos, TargetPosition: parent.Pos, BlizzardBasinElfState.Killed));
                }
                yield return $"{RoundNumber}";
            }
        }

        public IEnumerable<string> SolveSecondPart()
        {
            Reset();
            RoundNumber = 0;
            List<List<(int ParentId, (int X, int Y) Pos)>> tree = new() { new List<(int ParentId, (int X, int Y) Pos)>() { (0, EntrancePosition) } };
            foreach (var treeLevel in GetTreeLevels(EntrancePosition, ExitPosition).Concat(GetTreeLevels(ExitPosition, EntrancePosition).Concat(GetTreeLevels(EntrancePosition, ExitPosition))))
            {
                tree.Add(treeLevel);
                BlizzardsPositions = BlizzardsPositionAtTime(RoundNumber).ToList();
                Elves!.Clear();
                foreach (var child in tree[RoundNumber])
                {
                    var parent = tree[RoundNumber - 1][child.ParentId];
                    Elves.Add((StartingPosition: parent.Pos, TargetPosition: child.Pos, BlizzardBasinElfState.Possible));
                }
                var parentIds = tree[RoundNumber].Select(x => x.ParentId).ToHashSet();
                for (var parentId = 0; parentId < tree[RoundNumber - 1].Count; parentId++)
                {
                    if (parentIds.Contains(parentId))
                        continue;
                    var parent = tree[RoundNumber - 1][parentId];
                    Elves.Add((StartingPosition: parent.Pos, TargetPosition: parent.Pos, BlizzardBasinElfState.Killed));
                }
                yield return $"{RoundNumber}";
            }
        }

        private IEnumerable<List<(int ParentId, (int X, int Y) Pos)>> GetTreeLevels((int X, int Y) start, (int X, int Y) end)
        {
            var queue = new List<(int ParentId, (int X, int Y) Pos)>
            {
                (0, start)
            };
            var found = false;
            do
            {
                RoundNumber++;
                var blizzardsPosition = BlizzardsPositionAtTime(RoundNumber).Select(b => (b.Position.X, b.Position.Y)).ToHashSet();
                var newQueue = new List<(int ParentId, (int X, int Y) Pos)>();
                for (var parentId = 0; parentId < queue.Count && !found; parentId++)
                {
                    var (x, y) = queue[parentId].Pos;
                    foreach (var (dx, dy) in AllowedMoves)
                    {
                        var child = (X: x + dx, Y: y + dy);
                        if (child.Y < 0 || child.Y >= GridHeight)
                            continue;
                        var isBlockedByBlizzardsOrWalls = blizzardsPosition.Contains(child) || Walls!.Contains(child);
                        if (isBlockedByBlizzardsOrWalls)
                            continue;
                        bool isNodeAlreadyHere = newQueue.Any(x => x.Pos == child) || ((dx, dy) != (0, 0) && queue.Any(x => x.Pos == child));
                        if (isNodeAlreadyHere)
                            continue;
                        newQueue.Add((parentId, child));
                        if (child == end)
                        {
                            found = true;
                            break;
                        }
                    }
                }
                if (found)
                    newQueue = newQueue.Where(x => x.Pos == end).ToList();
                queue = newQueue;
                yield return queue;
            } while (!found && queue.Count > 0);
            if (queue.Count == 0)
                throw new InvalidDataException("No solution found");
        }
    }
}

