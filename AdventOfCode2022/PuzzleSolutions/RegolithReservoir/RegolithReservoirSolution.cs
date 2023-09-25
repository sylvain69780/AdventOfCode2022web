using AdventOfCode2022Solutions.PuzzleSolutions;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2022Solutions.PuzzleSolutions.RegolithReservoir
{
    public class RegolithReservoirSolution : IPuzzleSolutionIter
    {
        private string _puzzleInput = string.Empty;

        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }

        public IEnumerable<string> SolveFirstPart()
        {
            OccupiedPositions.Clear();
            InitialPositions.Clear();
            var paths = _puzzleInput.Split("\n").Select(x => x.Replace(" -> ", "#").Split('#')
                .Select(y => y.Split(','))
                .Select(y => (x: int.Parse(y[0]), y: int.Parse(y[1]))).ToList())
                .ToList();
            var floorPosition = paths.SelectMany(x => x).Select(x => x.y).Max() + 2;
            foreach (var rocks in paths)
            {
                for (var i = 0; i < rocks.Count - 1; i++)
                {
                    var beginRock = rocks[i];
                    var endRock = rocks[i + 1];
                    if (beginRock.y == endRock.y)
                        for (var x = Math.Min(beginRock.x, endRock.x); x <= Math.Max(beginRock.x, endRock.x); x++)
                            SetOccupiedInitial((x, beginRock.y));
                    if (beginRock.x == endRock.x)
                        for (var y = Math.Min(beginRock.y, endRock.y); y <= Math.Max(beginRock.y, endRock.y); y++)
                            SetOccupiedInitial((beginRock.x, y));
                }
            }

            var iterations = 0;
            while (true)
            {
                SandPosition = (500, 0);
                var isFreeToMove = true;
                while (isFreeToMove && SandPosition.y < floorPosition)
                {
                    var newSandPosition = SandPosition;
                    foreach (var (dx, dy) in Directions)
                    {
                        var (x, y) = (SandPosition.x + dx, SandPosition.y + dy);
                        if (!OccupiedPositions!.Contains((x, y)))
                        {
                            newSandPosition = (x, y);
                            break;
                        }
                    }
                    if (newSandPosition == SandPosition)
                        isFreeToMove = false;
                    else
                        SandPosition = newSandPosition;
                    yield return iterations.ToString();
                }
                if (SandPosition.y >= floorPosition)
                    break;
                SetOccupied(SandPosition);
                iterations++;
            }
            yield return iterations.ToString();
        }
        public IEnumerable<string> SolveSecondPart()
        {
            OccupiedPositions.Clear();
            InitialPositions.Clear();
            var paths = _puzzleInput.Split("\n").Select(x => x.Replace(" -> ", "#").Split('#')
                .Select(y => y.Split(','))
                .Select(y => (x: int.Parse(y[0]), y: int.Parse(y[1]))).ToList())
                .ToList();
            var floorPosition = paths.SelectMany(x => x).Select(x => x.y).Max() + 2;
            foreach (var rocks in paths)
            {
                for (var i = 0; i < rocks.Count - 1; i++)
                {
                    var beginRock = rocks[i];
                    var endRock = rocks[i + 1];
                    if (beginRock.y == endRock.y)
                        for (var x = Math.Min(beginRock.x, endRock.x); x <= Math.Max(beginRock.x, endRock.x); x++)
                            SetOccupiedInitial((x, beginRock.y));
                    if (beginRock.x == endRock.x)
                        for (var y = Math.Min(beginRock.y, endRock.y); y <= Math.Max(beginRock.y, endRock.y); y++)
                            SetOccupiedInitial((beginRock.x, y));
                }
            }
            var iterations = 0;
            while (true)
            {
                SandPosition = (500, 0);
                var isFreeToMove = true;
                while (isFreeToMove)
                {
                    var newSandPosition = SandPosition;
                    foreach (var (dx, dy) in Directions)
                    {
                        var (x, y) = (SandPosition.x + dx, SandPosition.y + dy);
                        if (y < floorPosition && !OccupiedPositions!.Contains((x, y)))
                        {
                            newSandPosition = (x, y);
                            break;
                        }
                    }
                    if (newSandPosition == SandPosition)
                        isFreeToMove = false;
                    else
                        SandPosition = newSandPosition;
                }
                iterations++;
                if (SandPosition == (500, 0))
                    break;
                SetOccupied(SandPosition);
                yield return iterations.ToString();
            }
            yield return iterations.ToString();
        }
        private static readonly (int x, int y)[] Directions = new (int x, int y)[] { (0, 1), (-1, 1), (1, 1) };

        public (int x, int y) SandPosition;
        public readonly HashSet<(int x, int y)> OccupiedPositions = new HashSet<(int x, int y)>();
        public readonly HashSet<(int x, int y)> InitialPositions = new HashSet<(int x, int y)>();
        public int xMin = 500;
        public int yMin;
        public int xMax = 500;
        public int yMax;
        public void SetOccupiedInitial((int x, int y) position)
        {
            OccupiedPositions!.Add(position);
            InitialPositions!.Add(position);
            xMin = Math.Min(xMin, position.x);
            yMin = Math.Min(yMin, position.y);
            xMax = Math.Max(xMax, position.x);
            yMax = Math.Max(yMax, position.y);
        }
        public void SetOccupied((int x, int y) position)
        {
            OccupiedPositions!.Add(position);
            xMin = Math.Min(xMin, position.x);
            yMin = Math.Min(yMin, position.y);
            xMax = Math.Max(xMax, position.x);
            yMax = Math.Max(yMax, position.y);
        }
    }
}