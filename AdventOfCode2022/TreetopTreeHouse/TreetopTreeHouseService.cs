using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TreetopTreeHouse
{
    public class TreetopTreeHouseService : SimplePuzzleService<TreetopTreeHouseModel>
    {
        public TreetopTreeHouseService(IEnumerable<IPuzzleStrategy<TreetopTreeHouseModel>> strategies) : base(strategies)
        {
        }
    }
}
