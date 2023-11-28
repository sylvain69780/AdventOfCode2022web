using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RegolithReservoir
{
    public class RegolithReservoirService : SimplePuzzleService<RegolithReservoirModel>
    {
        public RegolithReservoirService(IEnumerable<IPuzzleStrategy<RegolithReservoirModel>> strategies) : base(strategies)
        {
        }

        public HashSet<(int x, int y)> OccupiedPositions => _model.OccupiedPositions;
        public HashSet<(int x, int y)> InitialPositions => _model.InitialPositions;
        public (int x, int y) SandPosition => _model.SandPosition;
        public int xMin => _model.xMin;
        public int yMin => _model.yMin;
        public int xMax => _model.xMax;
        public int yMax => _model.yMax;

    }
}
