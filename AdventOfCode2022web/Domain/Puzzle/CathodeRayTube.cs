using System.Text;

namespace AdventOfCode2022web.Domain.Puzzle
{
    public class CathodeRayTube : PuzzleSolver
    {
        private static string Format(int v) => v.ToString();
        private static string[] ToLines(string s) => s.Split("\n");

        protected override string Part1(string puzzleInput)
        {
            var program = ToLines(puzzleInput);
            var numsToAdd = program.Select(x => x.Split(" "))
                .SelectMany(x => x[0] == "noop" ? new int[] { 0 } : new int[] { 0, int.Parse(x[1]) });
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
            return Format(sumOfSixSignalStrengths);
        }

        private static IEnumerable<int> GetProgramResults(IEnumerable<int> numsToAdd)
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

        protected override string Part2(string puzzleInput)
        {
            var program = ToLines(puzzleInput);
            var numsToAdd = program.Select(x => x.Split(" "))
                .SelectMany(x => x[0] == "noop" ? new int[] { 0 } : new int[] { 0, int.Parse(x[1]) });
            var programResults = GetProgramResults(numsToAdd).GetEnumerator();
            var messageLine = new StringBuilder();
            var message = new List<string>();
            foreach (var y in Enumerable.Range(0, 6))
            {
                messageLine.Clear();
                foreach (var x in Enumerable.Range(0, 40))
                    if (programResults.MoveNext() && programResults.Current >= x - 1 && programResults.Current <= x + 1)
                        messageLine.Append('#');
                    else
                        messageLine.Append('.');
                Console.WriteLine(messageLine);
                message.Add(messageLine.ToString());
            }
            return string.Join("\n", message);
        }
    }
}