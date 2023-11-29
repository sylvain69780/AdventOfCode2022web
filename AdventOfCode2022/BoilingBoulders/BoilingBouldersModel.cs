using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BoilingBoulders
{
    public class BoilingBouldersModel : IPuzzleModel
    {
        public List<Voxel>? Voxels;

        public void Parse(string puzzleInput)
        {
            Voxels = puzzleInput.Split("\n").Select(x => x.Split(','))
                .Select(x => x.Select(y => int.Parse(y)).ToArray())
                .Select(x => new Voxel(X: x[0], Y: x[1], Z: x[2])).ToList();
        }

        public static readonly List<Voxel> CubeFaces = new()
        {
                new Voxel(1,0,0),
                new Voxel(-1,0,0),
                new Voxel(0,1,0),
                new Voxel(0,-1,0),
                new Voxel(0,0,1),
                new Voxel(0,0,-1)
            };



    }
}
