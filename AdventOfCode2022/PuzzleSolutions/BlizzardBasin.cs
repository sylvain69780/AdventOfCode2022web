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
        Up = 3 ,
        Down = 4
    }

    //BlizzardsTypes = new char[] { '>', '<', '^', 'v' }

public interface IBlizzardBasinState
    {
        int GridWidth { get; }
        int GridHeight { get; }
        List<((int X, int Y) StartingPosition,(int X, int Y) TargetPosition,BlizzardBasinElfState State)>? Elves { get; }
        List<((int X, int Y) TargetPosition, Directions)>? Blizzards { get; }
        (int X, int Y) EntrancePosition { get; }
        (int X, int Y) ExitPosition { get;  }
    }

    [Puzzle(24, "Blizzard Basin",true)]
    public class BlizzardBasin : IPuzzleSolutionIter, IBlizzardBasinState
    {
        private int RoundNumber;
        public (int X, int Y) EntrancePosition { get; set; }
        public (int X, int Y) ExitPosition { get; set; }
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }

        private static readonly List<(int dx, int dy)> Moves = new()
        {
            (0,0),
            (1,0),
            (-1,0),
            (0,-1),
            (0,1),
        };

        public List<((int X, int Y) StartingPosition, (int X, int Y) TargetPosition, BlizzardBasinElfState State)>? Elves { get; }
        public List<((int X, int Y) TargetPosition, Directions)>? Blizzards { get; }

        public Dictionary<(int x, int y, int t), (int x, int y, int t)>? Prev;
        public List<(int x, int y)>? DeadEnds { get; set; }
        public bool ComputingCompleted;

        private List<(int x, int y, char c)>? BlizzardsRight;
        private List<(int x, int y, char c)>? BlizzardsLeft;
        private List<(int x, int y, char c)>? BlizzardsUp;
        private List<(int x, int y, char c)>? BlizzardsDown;
        private HashSet<(int x, int y)>? Walls;
        private string[]? Input { get; set; }

        public void Initialize(string puzzleInput)
        {
            Input = puzzleInput.Split("\n");
            Reset();
        }

        private void Reset()
        {
            EntrancePosition = ( Input![0].IndexOf('.'),  0);
            ExitPosition = ( Input[^1].IndexOf('.'), Input.Length - 1);
            Walls = Input
                .SelectMany((line, row) => line.Select((c, col) => (c, col, row))
                .Where(y => y.c == '#'))
                .Select(e => (x: e.col, y: e.row))
                .ToHashSet();
            var blizzards = Input
                .SelectMany((line, row) => line.Select((c, col) => (c, col, row))
                .Where(y => BlizzardsTypes.Contains(y.c)))
                .Select(e => (x: e.col, y: e.row, e.c))
                .ToList();
            BlizzardsRight = blizzards.Where(e => e.c == '>').ToList();
            BlizzardsLeft = blizzards.Where(e => e.c == '<').ToList();
            BlizzardsUp = blizzards.Where(e => e.c == '^').ToList();
            BlizzardsDown = blizzards.Where(e => e.c == 'v').ToList();
            GridWidth = Input[0].Length - 2;
            GridHeight = Input.Length - 2;

            RoundNumber = 0;
            ComputingCompleted = false;
            Prev = new Dictionary<(int x, int y, int t), (int x, int y, int t)>();
            DeadEnds = new List<(int x, int y)>();
        }

        private static readonly char[] BlizzardsTypes = new char[] { '>', '<', '^', 'v' };

        private static int Mod(int x, int m) => (x % m + m) % m;

        public IEnumerable<string> SolveFirstPart()
        {
            Reset();
            RoundNumber = 0;
            var newPrev = new Dictionary<(int x, int y, int t), (int x, int y, int t)>();
            ComputingCompleted = false;
            var search = new Queue<(int x, int y)>();
            Prev = new Dictionary<(int x, int y, int t), (int x, int y, int t)>();
            search.Enqueue(EntrancePosition);

        
            bool found;
            do
            {
                RoundNumber++;
                var newSearch = new Queue<(int x, int y)>();
                HashSet<(int, int y)> blizzardsPos = ComputeBlizzardsPos();
                found = SearchForNextMove(search,newSearch, blizzardsPos,ExitPosition);
                yield return $"{RoundNumber}";
                search = newSearch;
            } while (search.Count > 0 && !found);
            var p = (x:ExitPosition.X, y:ExitPosition.Y,t: RoundNumber);
            if (!Prev.ContainsKey(p))
                throw new InvalidDataException("No solution found");
            while (Prev.TryGetValue(p, out var np))
            {
                newPrev.Add(p, np);
                p = np;
            }
            Prev = newPrev;
            var minute = RoundNumber;
            for (var i = 1; i <= minute; i++) 
            {
                RoundNumber = i;
                yield return $"Replay step {i}";
            }
            ComputingCompleted = true;
            yield return $"{RoundNumber}";
        }

        private bool SearchForNextMove(Queue<(int x, int y)> search, Queue<(int x, int y)> newSearch, HashSet<(int, int y)> blizzardsPos,(int x,int y) arrival)
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
                    if ( !blizzardsPos.Contains(pos) && !Walls!.Contains(pos) && !newSearch.Contains(pos))
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

        public HashSet<(int x, int y)> ComputeBlizzardsPos()
        {
            // compute blizzards positions
            var blizzardsPos = BlizzardsRight!.Select(e => ((e.x - 1 + RoundNumber) % GridWidth + 1, e.y)).ToHashSet();
            blizzardsPos.UnionWith(BlizzardsLeft!.Select(e => (Mod(e.x - 1 - RoundNumber, GridWidth) + 1, e.y)));
            blizzardsPos.UnionWith(BlizzardsUp!.Select(e => (e.x, Mod(e.y - 1 - RoundNumber, GridHeight) + 1)));
            blizzardsPos.UnionWith(BlizzardsDown!.Select(e => (e.x, (e.y - 1 + RoundNumber) % GridHeight + 1)));
            return blizzardsPos;
        }

        public IEnumerable<string> SolveSecondPart()
        {
            Reset();
            RoundNumber = 0;
            ComputingCompleted = false;
            var newPrev = new Dictionary<(int x, int y, int t), (int x, int y, int t)>();
            var stages = new ((int x, int y), (int x, int y))[]
            {
                (EntrancePosition,ExitPosition),
                (ExitPosition,EntrancePosition),
                (EntrancePosition,ExitPosition),
            };
            foreach (var (start,arrival) in stages)
            {
                Prev = new Dictionary<(int x, int y, int t), (int x, int y, int t)>();
                var search = new Queue<(int x, int y)>();
                search.Enqueue(start);
                bool found;
                do
                {
                    RoundNumber++;
                    var newSearch = new Queue<(int x, int y)>();
                    HashSet<(int, int y)> blizzardsPos = ComputeBlizzardsPos();
                    found = SearchForNextMove(search, newSearch, blizzardsPos, arrival);
                    yield return $"{RoundNumber}";
                    search = newSearch;
                } while (search.Count > 0 && !found);
                var p = (arrival.x,arrival.y,RoundNumber);
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
            ComputingCompleted = true;
            yield return $"{RoundNumber}";
        }
    }
}