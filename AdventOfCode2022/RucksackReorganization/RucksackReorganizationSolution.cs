namespace sylvain69780.AdventOfCode2022.Domain.RucksackReorganization
{
    public class RucksackReorganizationSolution : IPuzzleSolution
    {
        private string _puzzleInput = string.Empty;
        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }
        private static string[] ToLines(string s) => s.Split("\n");
        private static string Format(int v) => v.ToString();

        /// <summary>
        /// Every item type can be converted to a priority
        /// Lowercase item types a through z have priorities 1 through 26.
        /// Uppercase item types A through Z have priorities 27 through 52.
        /// </summary>
        private static int Priority(char item) => item >= 'a' ? item - 'a' + 1 : item - 'A' + 27;

        public IEnumerable<string> SolveFirstPart()
        {
            var score = 0;
            foreach (var rucksack in ToLines(_puzzleInput))
            {
                var compartmentSize = rucksack.Length / 2;
                var (compartmentA, compartmentB) = (rucksack[..compartmentSize], rucksack[compartmentSize..(compartmentSize + compartmentSize)]);
                var sharedItem = compartmentA.First(x => compartmentB.Contains(x));
                score += Priority(sharedItem);
            }
           yield return Format(score);
        }

        public IEnumerable<string> SolveSecondPart()
        {
            var score = 0;
            var rucksacks = ToLines(_puzzleInput);
            for (var i = 0; i < rucksacks.Length / 3; i++)
            {
                var (firstGroup, secondGroup, thirdGroup) = (rucksacks[i * 3], rucksacks[i * 3 + 1], rucksacks[i * 3 + 2]);
                var badge = firstGroup.First(x => secondGroup.Contains(x) && thirdGroup.Contains(x));
                score += Priority(badge);
            }
            yield return Format(score);
        }
    }
}