using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.PipeMaze
{
    public class PipeMazePart2Strategy : IPuzzleStrategy<PipeMazeModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(PipeMazeModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var maze = model.Maze!;
            var start = maze
                .Select((line, y) => (line, y))
                .SelectMany(row => row.line.Select((c, x) => (c, x, row.y)))
                .Where(p => p.c == 'S').Select(p => (p.x, p.y)).First();
            var bfs = new Queue<(int x, int y, int id)>();
            bfs.Enqueue((start.x, start.y, 0));
            var visited = new int[maze[0].Length, maze.Length];
            model.Map = () => visited;
            var found = false;
            var dirs = new List<(int dx, int dy)>()
            {
                (0,1), // down
                (0,-1), // up
                (1,0), // right
                (-1,0) // left
            };
            var distance = 0;
            var path1 = (x: -1, y: -1, dist: 0);
            var path2 = (x: -1, y: -1, dist: 0);
            while (!found)
            {
                var newBfs = new Queue<(int x, int y, int id)>();
                while (bfs.TryDequeue(out var p))
                {
                    for (var i = 0; i < 4; i++)
                    {
                        var dir = dirs[i];
                        var pos = (x: p.x + dir.dx, y: p.y + dir.dy, id: p.id == 0 ? dir.dx * 2 + dir.dy : p.id);
                        var c = model.GetTile((pos.x, pos.y));
                        if (c == '.')
                            continue;
                        var isValidMove =
                            (dir == (1, 0) && (c == '-' || c == 'J' || c == '7'))
                         || (dir == (-1, 0) && (c == '-' || c == 'F' || c == 'L'))
                         || (dir == (0, 1) && (c == '|' || c == 'L' || c == 'J'))
                         || (dir == (0, -1) && (c == '|' || c == 'F' || c == '7'));
                        if (!isValidMove)
                            continue;
                        var otherEnd = bfs
                            .Where(a => (a.x, a.y) == (pos.x, pos.y) && a.id != pos.id).ToList();
                        var otherEnd2 = newBfs
                            .Where(a => (a.x, a.y) == (pos.x, pos.y) && a.id != pos.id).ToList();
                        if (otherEnd.Count != 0 || otherEnd2.Count != 0)
                        {
                            found = true;
                            path1 = (p.x, p.y, distance);
                            if (otherEnd.Count != 0)
                                path2 = (pos.x, pos.y, distance);
                            else
                                path2 = (pos.x, pos.y, distance+1);
                            break;
                        }
                        if (visited[pos.x, pos.y] > 0)
                            continue;
                        visited[pos.x, pos.y] = distance+1;
                        newBfs.Enqueue(pos);
                    }
                }
                distance++;
                yield return updateContext();
                if (newBfs.Count == 0)
                {
                    yield return updateContext();
                    provideSolution("No solution found.");
                    yield break;
                }
                bfs = newBfs;
            }
            // backtrace to identify the tiles part of the loop
            var loop = new List<(int x, int y)>();
            loop.Add(start);
            model.Loop = () => loop;
            {
                var p = (path1.x, path1.y);
                var d = path1.dist;
                while (d > 0)
                {
                    loop.Add(p);
                    yield return updateContext();
                    for (var i = 0; i < 4; i++)
                    {
                        var (dx, dy) = dirs[i];
                        var (x, y) = (p.x + dx,p.y + dy);
                        var c = model.GetTile((x, y));
                        if (c == '.')
                            continue;
                        if (visited[x,y] == d-1)
                        {
                            p = (x, y);
                            d--;
                            break;
                        }
                    }
                }
            }
            {
                var p = (path2.x, path2.y);
                var d = path2.dist;
                while (d > 0)
                {
                    loop.Add(p);
                    yield return updateContext();
                    for (var i = 0; i < 4; i++)
                    {
                        var (dx, dy) = dirs[i];
                        var (x, y) = (p.x + dx, p.y + dy);
                        var c = model.GetTile((x, y));
                        if (c == '.')
                            continue;
                        if (visited[x, y] == d - 1 && (x, y) != (path1.x,path1.y))
                        {
                            p = (x, y);
                            d--;
                            break;
                        }
                    }
                }
            }
            yield return updateContext();
            var loopHash = loop.ToHashSet();
            // find the tile of the S
            var guessStartTile = dirs.Where(x => loopHash.Contains((x.dx + start.x, x.dy + start.y)))
                .Where(x => visited[x.dx+start.x,x.dy+start.y] == 1).ToArray();
            var startTile = 'S';
            if (guessStartTile[0] == (0, 1) && guessStartTile[1] == (0, -1))
                startTile = '|';
            if (guessStartTile[0] == (0, 1) && guessStartTile[1] == (1, 0))
                startTile = 'F';
            if (guessStartTile[0] == (0, 1) && guessStartTile[1] == (-1, 0))
                startTile = '7';
            if (guessStartTile[0] == (0, -1) && guessStartTile[1] == (1, 0))
                startTile = 'L';
            if (guessStartTile[0] == (0, -1) && guessStartTile[1] == (-1, 0))
                startTile = 'J';
            maze[start.y]=maze[start.y].Replace('S', startTile);

            var count = 0;
            for (var y = 0; y < maze.Length; y++)
            {
                var inside = false;
                var lastC = ' ';
                for (var x = 0; x < maze[0].Length; x++)
                {
                    var onLoop = loopHash.Contains((x, y));
                    var c = model.GetTile((x, y));
                    if (onLoop)
                    {
                        if (c == '|' )
                            inside = !inside;
                        if ( c == 'J' && lastC == 'F')
                            inside = !inside;
                        if (c == '7' && lastC == 'L')
                            inside = !inside;
                        if (c == 'F' || c == 'L')
                            lastC = c;
                    } else
                    {
                        if (inside)
                        {
                            visited[x, y] = 50;
                            yield return updateContext();
                            count++;
                        }
                        else
                            visited[x, y] = 0;

                    }
                }
            }

            provideSolution(count.ToString());
        }
    }
}
