using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NoSpaceLeftOnDevice
{
    public class NoSpaceLeftOnDevicePart2Strategy : IPuzzleStrategy<NoSpaceLeftOnDeviceModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(NoSpaceLeftOnDeviceModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var directoriesContentSize = model.BuildDirectoriesContentSize();
            var totalDiskSize = 70000000;
            var freeSpaceRequired = 30000000;
            var totalSpaceUsed = directoriesContentSize["#/"];
            var toBeFreed = freeSpaceRequired - (totalDiskSize - totalSpaceUsed);
            var totalSizeOfDirectoryToBeDeleted = directoriesContentSize.Values.Where(x => x >= toBeFreed).Min();
            yield return updateContext();
            provideSolution(totalSizeOfDirectoryToBeDeleted.ToString());
        }
    }
}
