namespace AdventOfCode2022web.Domain.Puzzle
{
    public class TreetopTreeHouse : PuzzleSolver
    {
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
            public bool BorderReached(int x, int y) => x < 0 || x >= Width || y < 0 || y >= Height;
        }

        protected override string SolveFirst(string puzzleInput)
        {
            var map = new HeightMap(ToLines(puzzleInput));
            var visibleTrees = 0;
            foreach (var yTree in Enumerable.Range(0, map.Height))
                foreach (var xTree in Enumerable.Range(0, map.Width))
                {
                    var treeHeight = map.TreeHeight(xTree, yTree);
                    foreach (var (dx, dy) in Directions)
                    {
                        var distance = 1;
                        var (x, y) = (xTree + dx * distance, yTree + dy * distance);
                        var borderReached = map.BorderReached(x, y);
                        while (!borderReached && map.TreeHeight(x, y) < treeHeight)
                        {
                            distance++;
                            (x, y) = (xTree + dx * distance, yTree + dy * distance);
                            borderReached = map.BorderReached(x, y);
                        }
                        if (borderReached) { visibleTrees++; break; }
                    }
                }

            return Format(visibleTrees);
        }

        private static readonly (int, int)[] Directions = new (int x, int y)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };

        protected override string SolveSecond(string puzzleInput)
        {
            var map = new HeightMap(ToLines(puzzleInput));
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
                        while (!map.BorderReached(x, y) && map.TreeHeight(x, y) < treeHeight)
                        {
                            distance++;
                            (x, y) = (xTree + distance * dx, yTree + distance * dy);
                        }
                        score *= distance - (map.BorderReached(x, y) ? 1 : 0);
                    }
                    scoreMax = Math.Max(score, scoreMax);
                }
            return Format(scoreMax);
        }
    }
}