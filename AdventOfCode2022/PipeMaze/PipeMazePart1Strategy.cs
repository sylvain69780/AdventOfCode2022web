using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PipeMaze
{
    public class PipeMazePart1Strategy : IPuzzleStrategy<PipeMazeModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(PipeMazeModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var maze = model.Maze!;
            var start = maze
                .Select((line, y) => (line, y))
                .SelectMany(row => row.line.Select((c, x) => (c, x, row.y)))
                .Where(p => p.c == 'S').Select(p => (p.x, p.y)).First();
            var bfs = new Queue<(int x, int y, int id)>();
            bfs.Enqueue((start.x,start.y,0));
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
            while (!found)
            {
                var newBfs = new Queue<(int x, int y, int id)>();
                while (bfs.TryDequeue(out var p))
                {
                    for (var i = 0; i < 4; i++)
                    {
                        var dir = dirs[i];
                        var pos = (x: p.x + dir.dx, y: p.y + dir.dy,id: p.id ==0 ? dir.dx*2+dir.dy:p.id);
                        var c = model.GetTile((pos.x,pos.y));
                        if (c == '.')
                            continue;
                        var isValidMove =
                            (dir == (1, 0) && (c == '-' || c == 'J' || c == '7'))
                         || (dir == (-1, 0) && (c == '-' || c == 'F' || c == 'L'))
                         || (dir == (0, 1) && (c == '|' || c == 'L' || c == 'J'))
                         || (dir == (0, -1) && (c == '|' || c == 'F' || c == '7'));
                        if (!isValidMove)
                            continue;
                        if (
                            bfs.Any(a => (a.x,a.y) == (pos.x,pos.y) && a.id != pos.id) 
                            || newBfs.Any(a => (a.x, a.y) == (pos.x, pos.y) && a.id != pos.id))
                        {
                            found = true;
                            break;
                        }
                        if (visited[pos.x, pos.y] > 0)
                            continue;
                        visited[pos.x, pos.y] = distance;
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
            provideSolution(distance.ToString());
        }
    }
}
