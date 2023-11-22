using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class RucksackReorganizationTests
    {
        [Test]
        public void Part1_1()
        {
            var p = new RucksackReorganizationService(new RucksackReorganizationPart1Strategy());
            var i = p.GetStepsToSolution(input1).Last();
            Assert.That(p.Solution, Is.EqualTo("157"));
        }
        [Test]
        public void Part2_1()
        {
            var p = new RucksackReorganizationService(new RucksackReorganizationPart2Strategy());
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
