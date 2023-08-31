using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022web.Puzzles
{
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

    //BlizzardsTypes = new char[] { '>', '<', '^', 'v' }

    public interface IBlizzardBasinState
    {
        int GridWidth { get; }
        int GridHeight { get; }
        List<((int X, int Y) StartingPosition, (int X, int Y) TargetPosition, BlizzardBasinElfState State)>? Elves { get; }
        IEnumerable<((int X, int Y) Position, Directions Direction)>? BlizzardsPos { get; }
        (int X, int Y) EntrancePosition { get; }
        (int X, int Y) ExitPosition { get; }
    }

    [Puzzle(24, "Blizzard Basin", true)]
    public class BlizzardBasin : IPuzzleSolutionIter, IBlizzardBasinState
    {
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }
        public List<((int X, int Y) StartingPosition, (int X, int Y) TargetPosition, BlizzardBasinElfState State)>? Elves { get; set; }
        public (int X, int Y) EntrancePosition { get; set; }
        public (int X, int Y) ExitPosition { get; set; }

        public Dictionary<(int x, int y, int t), (int x, int y, int t)>? Prev;
        public List<(int x, int y)>? DeadEnds { get; set; }

        private int RoundNumber { get; set; }
        private bool IsSafeTraversalFound { get; set; }
        private Queue<(int X, int Y)>? BFS;
        private List<((int X, int Y) Position, Directions Direction)>? Blizzards { get; set; }
        public IEnumerable<((int X, int Y) Position, Directions Direction)> BlizzardsPos
            => Blizzards!
            .Select(b => (X: b.Position.X - 1, Y: b.Position.Y - 1, b.Direction))
            .Select(b => (X: b.X + RoundNumber * Moves[(int)b.Direction].dx, Y: b.Y + RoundNumber * Moves[(int)b.Direction].dy, b.Direction))
            .Select(b => (X: Mod(b.X, GridWidth-2), Y: Mod(b.Y, GridHeight-2), b.Direction))
            .Select(b => ((b.X + 1,b.Y + 1), b.Direction));

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
            Blizzards = Input!
                .SelectMany((line, row) => line.Select((c, col) => (c, col, row))
                .Where(y => "><^v".Contains(y.c)))
                .Select(e => ((x: e.col, y: e.row), e.c == '>' ? Directions.Right : e.c == '<' ? Directions.Left : e.c == '^' ? Directions.Up : Directions.Down))
                .ToList();
            RoundNumber = 0;
            Prev = new Dictionary<(int x, int y, int t), (int x, int y, int t)>();
            DeadEnds = new List<(int x, int y)>();
            BFS = new Queue<(int x, int y)>();
            EntrancePosition = (Input![0].IndexOf('.'), 0);
            ExitPosition = (Input[^1].IndexOf('.'), Input.Length - 1);
            GridWidth = Input[0].Length;
            GridHeight = Input.Length;
            Elves = new List<((int X, int Y) StartingPosition, (int X, int Y) TargetPosition, BlizzardBasinElfState State)>() 
            {
                (StartingPosition:EntrancePosition,TargetPosition:EntrancePosition , BlizzardBasinElfState.Safe) 
            };
        }

        private static readonly char[] BlizzardsTypes = new char[] { '>', '<', '^', 'v' };

        private static int Mod(int x, int m) => (x % m + m) % m;

        public IEnumerable<string> SolveFirstPart()
        {
            Reset();
            BFS!.Enqueue(EntrancePosition);
            do
            {
                RoundNumber++;
                var blizzardsPos = BlizzardsPos.Select(b => (b.Position.X,b.Position.Y)).ToHashSet();
                var bfs = new Queue<(int X, int Y)>();
                Elves!.Clear();
                {
                    while (BFS.TryDequeue(out var position))
                    {
                        var branches = new List<(int X,int Y)>();
                        for (var i = 0; i<Moves.Count; i++)
                        {
                            var (dx, dy) = Moves[i];
                            var pos = (X: position.X + dx, Y: position.Y + dy);
                            if (blizzardsPos.Contains(pos) || Walls!.Contains(pos) || bfs.Contains(pos) || (i > 0 && BFS.Contains(pos)) || pos.Y < 0 || pos.Y >= GridHeight )
                                continue;
                            else
                                branches.Add(pos);
                        }
                        // update state
                        if (branches.Count == 0)
                            Elves.Add((StartingPosition: position, TargetPosition: position, BlizzardBasinElfState.Killed));
                        else
                            foreach(var branch in branches)
                            {
                                Elves.Add((StartingPosition: position, TargetPosition: branch, BlizzardBasinElfState.Possible));
                                bfs.Enqueue(branch);
                            }
                    }
                }
                BFS = bfs;
                yield return $"Search phase {RoundNumber}";
            } while (BFS.Count > 0 && !BFS.Contains(ExitPosition));
            if (!BFS.Contains(ExitPosition))
                throw new InvalidDataException("No solution found");
            Elves!.Clear();
            foreach (var pos in BFS)
                Elves.Add((StartingPosition: pos, TargetPosition: pos, pos == ExitPosition ? BlizzardBasinElfState.Safe : BlizzardBasinElfState.Killed));
            yield return $"{RoundNumber}";
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

