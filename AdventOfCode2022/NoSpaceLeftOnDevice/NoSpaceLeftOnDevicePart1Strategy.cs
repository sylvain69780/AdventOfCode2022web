using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NoSpaceLeftOnDevice
{
    public class NoSpaceLeftOnDevicePart1Strategy : IPuzzleStrategy<NoSpaceLeftOnDeviceModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(NoSpaceLeftOnDeviceModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var directoriesContentSize = model.BuildDirectoriesContentSize();
            var sumOfTotalSizesOfDirectories = directoriesContentSize.Values.Where(x => x <= 100000).Sum();
            yield return updateContext();
            provideSolution(sumOfTotalSizesOfDirectories.ToString());
        }
    }
}
