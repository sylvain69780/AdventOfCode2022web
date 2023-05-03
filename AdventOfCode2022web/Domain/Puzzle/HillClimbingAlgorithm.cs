namespace AdventOfCode2022web.Domain.Puzzle
{
    public class HillClimbingAlgorithm : IPuzzleSolver
    {
        private class HillMap
        {
            public string[] Map;
            public (int x, int y) Start;
            public int Width;
            public int Height;

            public HillMap(string puzzleInput)
            {
                var input = puzzleInput.Split("\n");
                var start = (x: 0, y: 0);
                foreach (var l in input)
                {
                    var s = l.IndexOf('S');
                    if (s != -1)
                    {
                        start.x = s;
                        break;
                    }
                    start.y++;
                }
                Map = input;
                Start = start;
                Width = input[0].Length;
                Height = input.Length;
            }

            public int Altitude((int x, int y) p)
            {
                char c = Map[p.y][p.x];
                if (c == 'S') c = 'a';
                if (c == 'E') c = 'z';
                return (int)c - (int)'a';
            }

            public bool IsExit((int x, int y) p) => Map[p.y][p.x] == 'E';

            public bool IsOutOfMap((int x,int y ) p) => p.x < 0 || p.x >= Width || p.y < 0 || p.y >= Height;

            public IEnumerable<(int x, int y)> GetZeroHeighPositions()
            {
                for (var y = 0; y < Height; y++)
                    for (var x = 0; x < Width; x++)
                        if (Map[y][x] == 'a') yield return((x, y));
            }
        }

        static readonly List<(int x, int y)> Directions = new() { (1, 0), (-1, 0), (0, 1), (0, -1) };

        public IEnumerable<string> SolveFirstPart(string puzzleInput)
        {
            var map = new HillMap(puzzleInput);
            var exploredPositions = new HashSet<(int, int)>() { map.Start };
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
                        var nextPosition = (x: currentPosition.x + x, y: currentPosition.y + y);
                        if (map.IsOutOfMap(nextPosition) || exploredPositions.Contains(nextPosition)) continue;
                        if (map.Altitude(nextPosition) - map.Altitude(currentPosition) < 2)
                        {
                            newQueue.Enqueue(nextPosition);
                            exploredPositions.Add(nextPosition);
                            if (map.IsExit(nextPosition)) { newQueue.Clear(); breadthFirstSearchQueue.Clear(); break; }
                        }
                    }
                }
                breadthFirstSearchQueue = newQueue;
            }
            Console.WriteLine(exploredPositions.Count);
            yield return score.ToString();
        }
        public IEnumerable<string> SolveSecondPart(string puzzleInput)
        {
            var map = new HillMap(puzzleInput);
            var breadthFirstSearchQueue = new Queue<(int x, int y)>();
            foreach (var position in map.GetZeroHeighPositions())
                breadthFirstSearchQueue.Enqueue(position);
            var exploredPositions = new HashSet<(int, int)>() { map.Start };
            breadthFirstSearchQueue.Enqueue(map.Start);
            var score = 0;
            while (breadthFirstSearchQueue.Count > 0)
            {
                score++;
                var newQueue = new Queue<(int, int)>();
                while (breadthFirstSearchQueue.Count > 0)
                {
                    var currentPosition = breadthFirstSearchQueue.Dequeue();
                    foreach (var (dx, dy) in Directions)
                    {
                        var nextPosition = (x: currentPosition.x + dx, y: currentPosition.y + dy);
                        if (map.IsOutOfMap(nextPosition) || exploredPositions.Contains(nextPosition)) continue;
                        if (map.Altitude(nextPosition) - map.Altitude(currentPosition) < 2)
                        {
                            newQueue.Enqueue(nextPosition);
                            exploredPositions.Add(nextPosition);
                            if (map.IsExit(nextPosition)) 
                            { 
                                newQueue.Clear();
                                breadthFirstSearchQueue.Clear(); 
                                break; 
                            }
                        }
                    }
                }
                breadthFirstSearchQueue = newQueue;
            }
            Console.WriteLine(exploredPositions.Count);
            yield return score.ToString();
        }
    }
}