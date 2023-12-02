using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CubeConundrum
{
    public class CubeConundrumModel : IPuzzleModel
    {
        List<List<List<(int count, string color)>>>? _informationForEachGame;
        public List<List<List<(int count, string color)>>>? InformationForEachGame => _informationForEachGame;
        public void Parse(string input)
        {
            _informationForEachGame = input.Replace("\r", "").Split("\n")
                .Select(x => x.Split(':')[1])
                .Select(x => x.Split(';')
                    .Select(y => y.Split(',')
                        .Select(z => z.Split(' '))
                        .Select(z => (count: int.Parse(z[1]),Color : z[2]))
                        .ToList())
                    .ToList())
                .ToList();
        }
    }
}
