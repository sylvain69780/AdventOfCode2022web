using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class RucksackReorganizationTests
    {
        readonly List<IPuzzleStrategy<RucksackReorganizationModel>> _strategies = new()
            {
                new RucksackReorganizationPart1Strategy(),
            new RucksackReorganizationPart2Strategy()
            };

        [Test]
        public void Part1_1()
        {
            var p = new RucksackReorganizationService(_strategies);
            p.SetStrategy("Part 1");
            var i = p.GetStepsToSolution(input1).Last();
            Assert.That(p.Solution, Is.EqualTo("157"));
        }
        [Test]
        public void Part2_1()
        {
            var p = new RucksackReorganizationService(_strategies);
            p.SetStrategy("Part 2");
            var i = p.GetStepsToSolution(input1).Last();
            Assert.That(p.Solution, Is.EqualTo("70"));
        }

        string input1 = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";
    }
}
