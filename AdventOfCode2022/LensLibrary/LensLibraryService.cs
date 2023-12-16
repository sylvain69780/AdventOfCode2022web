using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LensLibrary
{
    public class LensLibraryService : SimplePuzzleService<LensLibraryModel>
    {
        public LensLibraryService(IEnumerable<IPuzzleStrategy<LensLibraryModel>> strategies) : base(strategies)
        {
        }
    }
}
