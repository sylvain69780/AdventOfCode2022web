using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HillClimbingAlgorithm
{
    public class HillClimbingAlgorithmService : SimplePuzzleService<HillClimbingAlgorithmModel>
    {
        public HillClimbingAlgorithmService(IEnumerable<IPuzzleStrategy<HillClimbingAlgorithmModel>> strategies) : base(strategies)
        {
        }
    }
}
