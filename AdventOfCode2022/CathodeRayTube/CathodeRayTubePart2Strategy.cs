using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CathodeRayTube
{
    public class CathodeRayTubePart2Strategy : IPuzzleStrategy<CathodeRayTubeModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(CathodeRayTubeModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var valuesOfXRegister = ComputeValuesOfXRegister(model.NumsToAdd!).GetEnumerator();
            var messageLine = new StringBuilder();
            var message = new List<string>();
            foreach (var _ in Enumerable.Range(0, 6))
            {
                messageLine.Clear();
                foreach (var pixelPosX in Enumerable.Range(0, 40))
                    if (valuesOfXRegister.MoveNext() && valuesOfXRegister.Current >= pixelPosX - 1 && valuesOfXRegister.Current <= pixelPosX + 1)
                        messageLine.Append('#');
                    else
                        messageLine.Append('.');
                message.Add(messageLine.ToString());
            }
            yield return updateContext();
            provideSolution(string.Join("\n", message));
        }

        private static IEnumerable<int> ComputeValuesOfXRegister(IEnumerable<int> numsToAdd)
        {
            var valueOfXregister = 1;
            var currentCycle = 0;
            foreach (var value in numsToAdd)
            {
                yield return valueOfXregister;
                currentCycle++;
                valueOfXregister += value;
            }
        }

    }
}
