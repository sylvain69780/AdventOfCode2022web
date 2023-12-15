using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ParabolicReflectorDish
{
    public class ParabolicReflectorDishModel : IPuzzleModel
    {
        char[,]? _platform;
        public char[,]? Platform => _platform;
        public void Parse(string input)
        {
            var inp = input.Replace("\r","").Split("\n");
            var width = inp[0].Length;
            var height = inp.Length;
            _platform = new char[width, height];
            for (var y = 0;y<height; y++)
            for (var x = 0; x<width;x++)
                {
                    _platform[x, y] = inp[y][x];
                }
        }
    }
}
