using System.Text;

namespace Domain.CathodeRayTube
{
    public class CathodeRayTubeSolution : IPuzzleSolution
    {
        private static string Format(int v) => v.ToString();

        private string[] program = Array.Empty<string>();

        public void Initialize(string s)
        {
            program = s.Split("\n");
        }

        public IEnumerable<string> SolveFirstPart()
        {
            var numsToAdd = program.Select(x => x.Split(" ")).SelectMany(x => x[0] == "noop" ? new int[] { 0 } : new int[] { 0, int.Parse(x[1]) });
            var valueOfXregister = 1;
            var cycleToRecord = 20;
            var currentCycle = 0;
            var sumOfSixSignalStrengths = 0;
            foreach (var value in numsToAdd)
            {
                currentCycle++;
                if (currentCycle == cycleToRecord)
                {
                    cycleToRecord += 40;
                    sumOfSixSignalStrengths += valueOfXregister * currentCycle;
                }
                valueOfXregister += value;
            }
            yield return Format(sumOfSixSignalStrengths);
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

        public IEnumerable<string> SolveSecondPart()
        {
            var numsToAdd = program.Select(x => x.Split(" "))
                .SelectMany(x => x[0] == "noop" ? new int[] { 0 } : new int[] { 0, int.Parse(x[1]) });
            var valuesOfXRegister = ComputeValuesOfXRegister(numsToAdd).GetEnumerator();
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
            yield return string.Join("\n", message);
        }
    }
}