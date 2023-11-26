using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TreetopTreeHouse
{
    public class TreetopTreeHouseModel : IPuzzleModel
    {
        private string _puzzleInput = string.Empty;

        public void Parse(string input)
        {
            _puzzleInput = input;
            Map = input.Split('\n');
        }

        public string[] Map { get; set; } = Array.Empty<string>();

        public int Width => Map[0].Length;
        public int Height => Map.Length;
        public int TreeHeight(int x, int y) => Map[y][x] - '0';
        public bool IsOutOfMap(int x, int y) => x < 0 || x >= Width || y < 0 || y >= Height;

        public static readonly (int, int)[] Directions = new (int x, int y)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };

    }
}
