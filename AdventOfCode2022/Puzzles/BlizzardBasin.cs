using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Versioning;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(24, "Blizzard Basin")]
    public class BlizzardBasin : IPuzzleSolverV3
    {
        public string Visualize()
        {
            return string.Empty;
        }

        public void Setup(string puzzleInput)
        {
            var input = puzzleInput.Split("\n");
            Start = (x: input[0].IndexOf('.'), y: 0);
            Arrival = (x: input[^1].IndexOf('.'), y: input.Length - 1);
            Walls = input
                .SelectMany((line, row) => line.Select((c, col) => (c, col, row))
                .Where(y => y.c == '#'))
                .Select(e => (x: e.col, y: e.row))
                .ToHashSet();
            var blizzards = input
                .SelectMany((line, row) => line.Select((c, col) => (c, col, row))
                .Where(y => BlizzardsTypes.Contains(y.c)))
                .Select(e => (x: e.col, y: e.row, e.c))
                .ToList();
            BlizzardsRight = blizzards.Where(e => e.c == '>').ToList();
            BlizzardsLeft = blizzards.Where(e => e.c == '<').ToList();
            BlizzardsUp = blizzards.Where(e => e.c == '^').ToList();
            BlizzardsDown = blizzards.Where(e => e.c == 'v').ToList();
            Width = input[0].Length - 2;
            Height = input.Length - 2;
        }

        public (int x, int y) Start;
        public (int x, int y) Arrival;
        public int Minute;
        public List<(int x, int y, char c)> BlizzardsRight;
        public List<(int x, int y, char c)> BlizzardsLeft;
        public List<(int x, int y, char c)> BlizzardsUp;
        public List<(int x, int y, char c)> BlizzardsDown;
        public int Width;
        public int Height;
        private HashSet<(int x, int y)> Walls;
        public Dictionary<(int x, int y, int t), (int x, int y, int t)>? Prev;
        public bool ComputingCompleted;

        private static readonly char[] BlizzardsTypes = new char[] { '>', '<', '^', 'v' };

        private static readonly List<(int dx, int dy)> Moves = new List<(int dx, int dy)>()
        {
            (0,0),
            (1,0),
            (-1,0),
            (0,1),
            (0,-1)
        };

        private int Mod(int x, int m) => (x % m + m) % m;

        public IEnumerable<string> SolveFirstPart()
        {
            Minute = 0;
            var newPrev = new Dictionary<(int x, int y, int t), (int x, int y, int t)>();
            ComputingCompleted = false;
            var search = new Queue<(int x, int y)>();
            Prev = new Dictionary<(int x, int y, int t), (int x, int y, int t)>();
            search.Enqueue(Start);

        
            bool found;
            do
            {
                Minute++;
                var newSearch = new Queue<(int x, int y)>();
                HashSet<(int, int y)> blizzardsPos = computeBlizzardsPos();
                found = SearchForNextMove(search,newSearch, blizzardsPos,Arrival);
                yield return $"{Minute}";
                search = newSearch;
            } while (search.Count > 0 && !found);
            var p = (Arrival.x, Arrival.y, Minute);
            if (!Prev.ContainsKey(p))
                throw new InvalidDataException("No solution found");
            while (Prev.TryGetValue(p, out var np))
            {
                newPrev.Add(p, np);
                p = np;
            }
            Prev = newPrev;
            ComputingCompleted = true;
            var minute = Minute;
            for (var i = 1; i <= minute; i++) 
            {
                Minute = i;
                yield return $"Replay step {i}";
            }
        }

        private bool SearchForNextMove(Queue<(int x, int y)> search, Queue<(int x, int y)> newSearch, HashSet<(int, int y)> blizzardsPos,(int x,int y) arrival)
        {
            bool found = false;
            while (search.TryDequeue(out var head))
            {
                foreach (var (dx, dy) in Moves)
                {
                    if (head == Start && dy == -1)
                        continue;
                    if (head == Arrival && dy == 1)
                        continue;

                    var pos = (x: head.x + dx, y: head.y + dy);
                    if (pos == arrival)
                    {
                        Prev.Add((pos.x, pos.y, Minute), (head.x, head.y, Minute - 1));
                        found = true;
                        break;
                    }
                    if ( !blizzardsPos.Contains(pos) && !Walls.Contains(pos) && !newSearch.Contains(pos))
                    {
                        Prev.Add((pos.x, pos.y, Minute), (head.x, head.y, Minute - 1));
                        newSearch.Enqueue(pos);
                    }
                }
                if (found)
                    break;
            }
            return found;
        }

        private HashSet<(int, int y)> computeBlizzardsPos()
        {
            // compute blizzards positions
            var blizzardsPos = BlizzardsRight.Select(e => ((e.x - 1 + Minute) % Width + 1, e.y)).ToHashSet();
            blizzardsPos.UnionWith(BlizzardsLeft.Select(e => (Mod(e.x - 1 - Minute, Width) + 1, e.y)));
            blizzardsPos.UnionWith(BlizzardsUp.Select(e => (e.x, Mod(e.y - 1 - Minute, Height) + 1)));
            blizzardsPos.UnionWith(BlizzardsDown.Select(e => (e.x, (e.y - 1 + Minute) % Height + 1)));
            return blizzardsPos;
        }

        public IEnumerable<string> SolveSecondPart()
        {
            Minute = 0;
            ComputingCompleted = false;
            var newPrev = new Dictionary<(int x, int y, int t), (int x, int y, int t)>();
            var stages = new ((int x, int y), (int x, int y))[]
            {
                (Start,Arrival),
                (Arrival,Start),
                (Start,Arrival),
            };
            foreach (var (start,arrival) in stages)
            {
                Prev = new Dictionary<(int x, int y, int t), (int x, int y, int t)>();
                var search = new Queue<(int x, int y)>();
                search.Enqueue(start);
                bool found;
                do
                {
                    Minute++;
                    var newSearch = new Queue<(int x, int y)>();
                    HashSet<(int, int y)> blizzardsPos = computeBlizzardsPos();
                    found = SearchForNextMove(search, newSearch, blizzardsPos, arrival);
                    yield return $"{Minute}";
                    search = newSearch;
                } while (search.Count > 0 && !found);
                var p = (arrival.x,arrival.y,Minute);
                if (!Prev.ContainsKey(p))
                    throw new InvalidDataException("No solution found");
                while (Prev.TryGetValue(p, out var np))
                {
                    newPrev.Add(p, np);
                    p = np;
                }
            }
            Prev = newPrev;
            ComputingCompleted = true;
            var minute = Minute;
            for (var i = 1; i <= minute; i++)
            {
                Minute = i;
                yield return $"Replay step {i}";
            }
        }
    }
}