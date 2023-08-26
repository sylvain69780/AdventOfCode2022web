using System.Diagnostics;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(12, "Hill Climbing Algorithm")]
    public class HillClimbingAlgorithm : IPuzzleSolution
    {
        private string _puzzleInput = string.Empty;
        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }

        private class HillMap
        {
            public string[] Map;
            public (int x, int y) Start;
            public int Width;
            public int Height;

            public HillMap(string puzzleInput)
            {
                Map = puzzleInput.Split("\n");
                Start = (0, 0);
                foreach (var l in Map)
                {
                    var s = l.IndexOf('S');
                    if (s != -1)
                    {
                        Start.x = s;
                        break;
                    }
                    Start.y++;
                }
                Width = Map[0].Length;
                Height = Map.Length;
                ExploredPositions = new HashSet<(int, int)>();
            }

            public int Altitude((int x, int y) p)
            {
                char c = Map[p.y][p.x];
                if (c == 'S') c = 'a';
                if (c == 'E') c = 'z';
                return c - 'a';
            }

            public bool IsExit((int x, int y) p) => Map[p.y][p.x] == 'E';

            public bool IsOutOfMap((int x, int y) p) => p.x < 0 || p.x >= Width || p.y < 0 || p.y >= Height;

            public IEnumerable<(int x, int y)> GetZeroHeighPositions()
            {
                for (var y = 0; y < Height; y++)
                    for (var x = 0; x < Width; x++)
                        if (Map[y][x] == 'a') yield return (x, y);
            }

            public HashSet<(int, int)> ExploredPositions;

            public void SetAsExplored((int x, int y) p)
            {
                ExploredPositions.Add(p);
            }

            public bool IsExploredPosition((int x, int y) position)
                => ExploredPositions.Contains(position);
        }

        static readonly List<(int x, int y)> Directions = new() { (1, 0), (-1, 0), (0, 1), (0, -1) };



        public string SolveFirstPart()
        {
            var map = new HillMap(_puzzleInput);
            map.SetAsExplored(map.Start);
            var breadthFirstSearchQueue = new Queue<(int x, int y)>();
            breadthFirstSearchQueue.Enqueue(map.Start);
            var score = 0;
            while (breadthFirstSearchQueue.Count > 0)
            {
                score++;
                var newQueue = new Queue<(int, int)>();
                while (breadthFirstSearchQueue.Count > 0)
                {
                    var currentPosition = breadthFirstSearchQueue.Dequeue();
                    foreach (var (x, y) in Directions)
                    {
                        var nextPosition = (currentPosition.x + x, currentPosition.y + y);
                        if (map.IsOutOfMap(nextPosition) || map.IsExploredPosition(nextPosition))
                            continue;
                        if (map.Altitude(nextPosition) - map.Altitude(currentPosition) >= 2)
                            continue;
                        if (map.IsExit(nextPosition))
                        {
                            newQueue.Clear();
                            breadthFirstSearchQueue.Clear();
                            break;
                        }
                        newQueue.Enqueue(nextPosition);
                        map.SetAsExplored(nextPosition);
                    }
                }
                breadthFirstSearchQueue = newQueue;
            }
            return score.ToString();
        }
        public string SolveSecondPart()
        {
            var map = new HillMap(_puzzleInput);
            map.SetAsExplored(map.Start);
            var breadthFirstSearchQueue = new Queue<(int x, int y)>();
            foreach (var position in map.GetZeroHeighPositions())
                breadthFirstSearchQueue.Enqueue(position);
            var distance = 1;
            var newQueue = new Queue<(int, int)>();
            while (breadthFirstSearchQueue.TryDequeue(out var currentPosition))
            {
                foreach (var nextPosition in Directions.Select(d => (currentPosition.x + d.x, currentPosition.y + d.y)))
                {
                    if (map.IsOutOfMap(nextPosition) || map.IsExploredPosition(nextPosition)
                        || map.Altitude(nextPosition) - map.Altitude(currentPosition) >= 2)
                        continue;
                    if (map.IsExit(nextPosition))
                    {
                        return distance.ToString();
                    }
                    newQueue.Enqueue(nextPosition);
                    map.SetAsExplored(nextPosition);
                }
                if (breadthFirstSearchQueue.Count == 0)
                {
                    distance++;
                    while (newQueue.TryDequeue(out var item))
                        breadthFirstSearchQueue.Enqueue(item);
                }
            }
            return "Not found " + distance.ToString();
        }
    }
}