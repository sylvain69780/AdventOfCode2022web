
namespace AdventOfCode2022web.Domain.Puzzle
{
    public class RucksackReorganization : PuzzleSolver
    {
        private static string[] ToLines(string s) => s.Split("\n");
        private static string Format(int v) => v.ToString();

        /// <summary>
        /// Every item type can be converted to a priority
        /// Lowercase item types a through z have priorities 1 through 26.
        /// Uppercase item types A through Z have priorities 27 through 52.
        /// </summary>
        private static int Priority(char item) => item >= 'a' ? item - 'a' + 1 : item - 'A' + 27;

        protected override string SolveFirst(string puzzleInput)
        {
            var score = 0;
            foreach (var rucksack in ToLines(puzzleInput))
            {
                var compartmentSize = rucksack.Length / 2;
                var (compartmentA, compartmentB) = (rucksack[..compartmentSize], rucksack[compartmentSize..(compartmentSize+compartmentSize)]);
                var sharedItem = compartmentA.First(x => compartmentB.Contains(x));
                score += Priority(sharedItem);
            }
            return Format(score);
        }

        protected override string SolveSecond(string puzzleInput)
        {
            var score = 0;
            var rucksacks = ToLines(puzzleInput);
            for (var i = 0; i < rucksacks.Length / 3; i++)
            {
                var (firstGroup, secondGroup, thirdGroup) = (rucksacks[i * 3], rucksacks[i * 3 + 1], rucksacks[i * 3 + 2]);
                var badge = firstGroup.First(x => secondGroup.Contains(x) && thirdGroup.Contains(x));
                score += Priority(badge);
            }
            return Format(score);
        }
    }
}