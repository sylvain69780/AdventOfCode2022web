using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ParabolicReflectorDish
{
    public class ParabolicReflectorDishService : SimplePuzzleService<ParabolicReflectorDishModel>
    {
        public ParabolicReflectorDishService(IEnumerable<IPuzzleStrategy<ParabolicReflectorDishModel>> strategies) : base(strategies)
        {
        }
    }
}
