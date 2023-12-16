using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.LensLibrary
{
    public class LensLibraryPart1Strategy : IPuzzleStrategy<LensLibraryModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(LensLibraryModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var initSeq = model.InitializationSequence!;
            var r = initSeq.Select(s => LensLibraryHelpers.Hash(s)).Sum();
            yield return updateContext();
            provideSolution(r.ToString());
        }
    }
}
