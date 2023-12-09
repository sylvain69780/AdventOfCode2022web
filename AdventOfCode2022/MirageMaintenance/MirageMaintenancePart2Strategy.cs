using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MirageMaintenance
{
    public class MirageMaintenancePart2Strategy : IPuzzleStrategy<MirageMaintenanceModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(MirageMaintenanceModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var historicalData = model.HistoricalData;
            Array.ForEach(historicalData, x => x.Reverse());
            MirageMaintenanceModel.AddNextValue(historicalData);
            yield return updateContext();
            provideSolution(historicalData.Select(x => x[^1]).Sum().ToString());
        }

    }
}
