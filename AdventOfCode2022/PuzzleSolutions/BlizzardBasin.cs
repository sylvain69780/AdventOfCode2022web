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
            .Select(b => (X: b.X + roundNumber * Moves[(int)b.Direction].dx, Y: b.Y + roundNumber * Moves[(int)b.Direction].dy, b.Direction))
            .Select(b => (X: Mod(b.X, GridWidth - 2), Y: Mod(b.Y, GridHeight - 2), b.Direction))
            .Select(b => ((b.X + 1, b.Y + 1), b.Direction));

        private static readonly List<(int dx, int dy)> Moves = new()
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
            var (start, end) = (EntrancePosition, ExitPosition);
            List<(int Id, int ParentId, (int X, int Y) Pos)> tree = ComputeSearchTree(start, end);

            var parentIds = new List<int>() { 0 };
            var treeGroupedByParent = tree.GroupBy(x => x.ParentId).ToDictionary(x => x.Key);
            for (var i = 0; i < RoundNumber; i++)
            {
                BlizzardsPositions = BlizzardsPositionAtTime(i + 1).ToList();
                Elves!.Clear();
                var newParentIds = new List<int>();
                foreach(var parentId in parentIds)
                {
                    var parent = tree[parentId];
                    if (treeGroupedByParent.TryGetValue(parentId, out var children))
                    {
                        foreach (var child in children)
                        {
                            Elves.Add((StartingPosition: parent.Pos, TargetPosition: child.Pos, BlizzardBasinElfState.Possible));
                            newParentIds.Add(child.Id);
                        }
                    } 
                    else
                    {
                        Elves.Add((StartingPosition: parent.Pos, TargetPosition: parent.Pos, BlizzardBasinElfState.Killed));
                    }
                }
                parentIds = newParentIds;
                yield return $"{i + 1}";
            }
        }

        private List<(int Id, int ParentId, (int X, int Y) Pos)> ComputeSearchTree((int X, int Y) start, (int X, int Y) end)
        {
            var found = false;
            var queue = new Queue<(int Id, (int X, int Y) Pos)>();
            queue.Enqueue((0, start));
            var tree = new List<(int Id, int ParentId, (int X, int Y) Pos)>
            {
                (0, -1, start)
            };
            do
            {
                RoundNumber++;
                var blizzardsPosition = BlizzardsPositionAtTime(RoundNumber).Select(b => (b.Position.X, b.Position.Y)).ToHashSet();
                var newQueue = new Queue<(int Id, (int X, int Y) Pos)>();
                {
                    while (queue.TryDequeue(out var position))
                    {
                        var possiblePaths = new List<(int X, int Y)>();
                        for (var i = 0; i < Moves.Count; i++)
                        {
                            var (dx, dy) = Moves[i];
                            var pos = (X: position.Pos.X + dx, Y: position.Pos.Y + dy);
                            var isPositionBlocked = blizzardsPosition.Contains(pos) || Walls!.Contains(pos) || newQueue.Any(x => x.Pos == pos) || (i > 0 && queue.Any(x => x.Pos == pos)) || pos.Y < 0 || pos.Y >= GridHeight;
                            if (!isPositionBlocked)
                                possiblePaths.Add(pos);
                        }
                        foreach (var possiblePath in possiblePaths)
                        {
                            newQueue.Enqueue((tree.Count, possiblePath));
                            tree.Add((tree.Count, position.Id, possiblePath));
                        }
                    }
                }
                queue = newQueue;
                found = queue.Any(x => x.Pos == end);
            } while (queue.Count > 0 && !found);
            if (!found)
                throw new InvalidDataException("No solution found");
            return tree;
        }

        private void AddAsKilledOrSafe((int X, int Y) pos)
        {
            Elves!.Add((StartingPosition: pos, TargetPosition: pos, pos == ExitPosition ? BlizzardBasinElfState.Safe : BlizzardBasinElfState.Killed));
        }

        private void AddAsPossiblePath((int X, int Y) position, (int X, int Y) branch)
        {
            Elves!.Add((StartingPosition: position, TargetPosition: branch, BlizzardBasinElfState.Possible));
        }

        private void AddAsKilled((int X, int Y) position)
        {
            Elves!.Add((StartingPosition: position, TargetPosition: position, BlizzardBasinElfState.Killed));
        }

        private void ResetViewModel()
        {
            Elves!.Clear();
            BlizzardsPositions = BlizzardsPositionAtTime(RoundNumber).ToList();
        }

        public IEnumerable<string> SolveSecondPart()
        {
            Reset();
            var newPrev = new Dictionary<(int x, int y, int t), (int x, int y, int t)>();
            var stages = new ((int x, int y), (int x, int y))[]
            {
                (EntrancePosition,ExitPosition),
                (ExitPosition,EntrancePosition),
                (EntrancePosition,ExitPosition),
            };
            foreach (var (start, arrival) in stages)
            {
                Prev = new Dictionary<(int x, int y, int t), (int x, int y, int t)>();
                var search = new Queue<(int x, int y)>();
                search.Enqueue(start);
                bool found;
                do
                {
                    RoundNumber++;
                    var newSearch = new Queue<(int x, int y)>();
                    /// to do
                    HashSet<(int, int y)> blizzardsPos = new HashSet<(int, int y)>();
                    found = SearchForNextMove(search, newSearch, blizzardsPos, arrival);
                    yield return $"{RoundNumber}";
                    search = newSearch;
                } while (search.Count > 0 && !found);
                var p = (arrival.x, arrival.y, RoundNumber);
                if (!Prev.ContainsKey(p))
                    throw new InvalidDataException("No solution found");
                while (Prev.TryGetValue(p, out var np))
                {
                    newPrev.Add(p, np);
                    p = np;
                }
            }
            Prev = newPrev;
            var minute = RoundNumber;
            for (var i = 1; i <= minute; i++)
            {
                RoundNumber = i;
                yield return $"Replay step {i}";
            }
            yield return $"{RoundNumber}";
        }

        private bool SearchForNextMove(Queue<(int x, int y)> search, Queue<(int x, int y)> newSearch, HashSet<(int, int y)> blizzardsPos, (int x, int y) arrival)
        {
            bool found = false;
            DeadEnds = new List<(int x, int y)>();
            while (search.TryDequeue(out var head))
            {
                var branches = 0;
                foreach (var (dx, dy) in Moves)
                {
                    if (head == EntrancePosition && dy == -1)
                        continue;
                    if (head == ExitPosition && dy == 1)
                        continue;

                    var pos = (x: head.x + dx, y: head.y + dy);
                    if (pos == arrival)
                    {
                        Prev!.Add((pos.x, pos.y, RoundNumber), (head.x, head.y, RoundNumber - 1));
                        found = true;
                        break;
                    }
                    if (!blizzardsPos.Contains(pos) && !Walls!.Contains(pos) && !newSearch.Contains(pos))
                    {
                        Prev!.Add((pos.x, pos.y, RoundNumber), (head.x, head.y, RoundNumber - 1));
                        newSearch.Enqueue(pos);
                        branches++;
                    }
                }
                if (branches == 0)
                {
                    DeadEnds.Add(head);
                }
                if (found)
                    break;
            }
            return found;
        }

    }
}

