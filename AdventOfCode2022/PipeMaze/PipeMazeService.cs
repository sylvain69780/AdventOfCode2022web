using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PipeMaze
{
    public class PipeMazeService : SimplePuzzleService<PipeMazeModel>
    {
        public Func<int[,]>? Map => _model.Map;
        public Func<List<(int x, int y)>>? Loop => _model.Loop;

        public PipeMazeService(IEnumerable<IPuzzleStrategy<PipeMazeModel>> strategies) : base(strategies)
        {
        }
    }
}
