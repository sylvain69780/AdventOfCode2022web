using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PipeMaze
{
    public class PipeMazeModel : IPuzzleModel
    {
        string[]? _maze;
        public string[]? Maze => _maze;

        public Func<int[,]>? Map;
        public Func<List<(int x,int y)>>? Loop;

        public void Parse(string input)
        {
            _maze = input.Replace("\r", "").Split("\n");

            var pipeTypes = new Dictionary<char, (bool right, bool left, bool up, bool down)>()
            {
                { '|' , (false,false,true,true) },
                { '-' , (true,true,false,false) },
                { 'L' , (true,false,true,false) },
                { 'J' , (false,true,true,false) },
                { '7' , (false,true,false,true) },
                { 'F' , (true,false,false,true) },
                { '.' , (false,false,false,false) },
            };
        }

        public char GetTile((int x,int y) pos)
        {
            var maze = _maze!;
            if (pos.x < 0 || pos.x >= maze[0].Length)
                return '.';
            if (pos.y < 0 || pos.y >= maze.Length)
                return '.';
            return maze[pos.y][pos.x];
        }

    }
}
