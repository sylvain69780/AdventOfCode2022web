using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RucksackReorganization
{
    public class RucksackReorganizationService : SimplePuzzleService<RuchsackReorganizationModel>
    {
        public RucksackReorganizationService(IPuzzleStrategy<RuchsackReorganizationModel> strategy) : base(strategy)
        {
        }
    }
}
