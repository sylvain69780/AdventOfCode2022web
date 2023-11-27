using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HillClimbingAlgorithm
{
    public class HillClimbingAlgorithmModel : IPuzzleModel
    {
        public string[]? Map;
        public (int x, int y) Start;
        public int Width;
        public int Height;

        public void Parse(string puzzleInput)
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
            char c = Map![p.y][p.x];
            if (c == 'S') c = 'a';
            if (c == 'E') c = 'z';
            return c - 'a';
        }

        public bool IsExit((int x, int y) p) => Map![p.y][p.x] == 'E';

        public bool IsOutOfMap((int x, int y) p) => p.x < 0 || p.x >= Width || p.y < 0 || p.y >= Height;

        public IEnumerable<(int x, int y)> GetZeroHeighPositions()
        {
            for (var y = 0; y < Height; y++)
                for (var x = 0; x < Width; x++)
                    if (Map![y][x] == 'a') yield return (x, y);
        }

        public HashSet<(int, int)>? ExploredPositions;

        public void SetAsExplored((int x, int y) p)
        {
            ExploredPositions!.Add(p);
        }

        public bool IsExploredPosition((int x, int y) position)
            => ExploredPositions!.Contains(position);

        public static readonly List<(int x, int y)> Directions = new() { (1, 0), (-1, 0), (0, 1), (0, -1) };

    }
}
