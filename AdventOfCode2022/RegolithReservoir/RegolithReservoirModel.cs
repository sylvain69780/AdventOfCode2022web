using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RegolithReservoir
{
    public class RegolithReservoirModel : IPuzzleModel
    {
        private string _puzzleInput = string.Empty;
        public string PuzzleInput => _puzzleInput;
        public void Parse(string input)
        {
            _puzzleInput = input;
        }

        public static readonly (int x, int y)[] Directions = new (int x, int y)[] { (0, 1), (-1, 1), (1, 1) };

        public (int x, int y) SandPosition;
        public readonly HashSet<(int x, int y)> OccupiedPositions = new();
        public readonly HashSet<(int x, int y)> InitialPositions = new();
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
