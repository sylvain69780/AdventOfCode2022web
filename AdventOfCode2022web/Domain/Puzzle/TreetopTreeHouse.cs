namespace AdventOfCode2022web.Domain.Puzzle
{
    public class TreetopTreeHouse : PuzzleSolver
    {
        private static string[] ToLines(string s) => s.Split("\n");
        private static string Format(int v) => v.ToString();


        private class HeightMap
        {
            public string[] Map { get; set; } = new string[0];

            public HeightMap(string[] map)
            {
                Map = map;
            }
            public int Width => Map[0].Length;
            public int Height => Map.Length;
            public int TreeHeight(int x, int y) => (int)Map[y][x] - (int)'0';
            public bool BorderReached(int x, int y) => x < 0 || x >= Width || y < 0 || y >= Height;
        }

        protected override string Part1(string puzzleInput)
        {
            var map = new HeightMap(ToLines(puzzleInput));
            var visibleTrees = new HashSet<(int x, int y)>();
            foreach (var y in Enumerable.Range(0, map.Height))
            {
                // left to right
                var hmax = -1;
                foreach (var x in Enumerable.Range(0, map.Width))
                {
                    var h = map.TreeHeight(x, y);
                    if (h > hmax)
                    {
                        hmax = h;
                        if (!visibleTrees.Contains((x, y))) visibleTrees.Add((x, y));
                    }
                }
                // right to left
                hmax = -1;
                foreach (var x in Enumerable.Range(0, map.Width).Reverse())
                {
                    var h = map.TreeHeight(x, y);
                    if (h > hmax)
                    {
                        hmax = h;
                        if (!visibleTrees.Contains((x, y))) visibleTrees.Add((x, y));
                    }
                }
            }
            foreach (var x in Enumerable.Range(0, map.Width))
            {
                // top to down
                var hmax = -1;
                foreach (var y in Enumerable.Range(0, map.Height))
                {
                    var h = map.TreeHeight(x, y);
                    if (h > hmax)
                    {
                        hmax = h;
                        if (!visibleTrees.Contains((x, y))) visibleTrees.Add((x, y));
                    }
                }
                // down to top
                hmax = -1;
                foreach (var y in Enumerable.Range(0, map.Height).Reverse())
                {
                    var h = map.TreeHeight(x, y);
                    if (h > hmax)
                    {
                        hmax = h;
                        if (!visibleTrees.Contains((x, y))) visibleTrees.Add((x, y));
                    }
                }
            }
            return Format(visibleTrees.Count);
        }

        private static readonly (int, int)[] Directions = new (int x, int y)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };

        protected override string Part2(string puzzleInput)
        {
            var map = new HeightMap(ToLines(puzzleInput));
            var scoreMax = 0;
            foreach (var yTree in Enumerable.Range(0, map.Height))
                foreach (var xTree in Enumerable.Range(0, map.Width))
                {
                    var treeHeight = map.TreeHeight( xTree, yTree);
                    var score = 1;
                    foreach (var (dx, dy) in Directions)
                    {
                        var distance = 1;
                        var (x, y) = (xTree + distance * dx, yTree + distance * dy);
                        while ( !map.BorderReached(x, y) && map.TreeHeight(x, y) < treeHeight ) 
                        {
                            distance++;
                            (x, y) = (xTree + distance * dx, yTree + distance * dy);
                        } 
                        score *= distance - (map.BorderReached(x, y) ? 1:0);
                    }
                    scoreMax = Math.Max(score,scoreMax);
                }
            return Format(scoreMax);
        }
    }
}