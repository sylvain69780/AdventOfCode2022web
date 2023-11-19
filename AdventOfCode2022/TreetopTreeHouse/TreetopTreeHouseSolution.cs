namespace Domain.TreetopTreeHouse
{
    public class TreetopTreeHouseSolution : IPuzzleSolution
    {
        private string _puzzleInput = string.Empty;
        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }
        private static string[] ToLines(string s) => s.Split("\n");
        private static string Format(int v) => v.ToString();

        private class HeightMap
        {
            public string[] Map { get; set; } = Array.Empty<string>();

            public HeightMap(string[] map)
            {
                Map = map;
            }
            public int Width => Map[0].Length;
            public int Height => Map.Length;
            public int TreeHeight(int x, int y) => Map[y][x] - '0';
            public bool IsOutOfMap(int x, int y) => x < 0 || x >= Width || y < 0 || y >= Height;
        }

        private static readonly (int, int)[] Directions = new (int x, int y)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };

        public IEnumerable<string> SolveFirstPart()
        {
            var map = new HeightMap(ToLines(_puzzleInput));
            var visibleTrees = 0;
            foreach (var yTree in Enumerable.Range(0, map.Height))
                foreach (var xTree in Enumerable.Range(0, map.Width))
                {
                    var treeHeight = map.TreeHeight(xTree, yTree);
                    foreach (var (dx, dy) in Directions)
                    {
                        var distance = 0;
                        bool borderReached;
                        var (x, y) = (0, 0);
                        do
                        {
                            distance++;
                            (x, y) = (xTree + dx * distance, yTree + dy * distance);
                            borderReached = map.IsOutOfMap(x, y);
                        } while (!borderReached && map.TreeHeight(x, y) < treeHeight);
                        if (borderReached)
                        {
                            visibleTrees++;
                            break;
                        }
                    }
                }
            yield return Format(visibleTrees);
        }

        public IEnumerable<string> SolveSecondPart()
        {
            var map = new HeightMap(ToLines(_puzzleInput));
            var scoreMax = 0;
            foreach (var yTree in Enumerable.Range(0, map.Height))
                foreach (var xTree in Enumerable.Range(0, map.Width))
                {
                    var treeHeight = map.TreeHeight(xTree, yTree);
                    var score = 1;
                    foreach (var (dx, dy) in Directions)
                    {
                        var distance = 1;
                        var (x, y) = (xTree + distance * dx, yTree + distance * dy);
                        while (!map.IsOutOfMap(x, y) && map.TreeHeight(x, y) < treeHeight)
                        {
                            distance++;
                            (x, y) = (xTree + distance * dx, yTree + distance * dy);
                        }
                        score *= distance - (map.IsOutOfMap(x, y) ? 1 : 0);
                    }
                    scoreMax = Math.Max(score, scoreMax);
                }
            yield return Format(scoreMax);
        }
    }
}