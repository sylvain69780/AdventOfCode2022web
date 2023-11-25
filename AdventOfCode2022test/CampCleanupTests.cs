using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class CampCleanupTests
    {
        readonly List<IPuzzleStrategy<CampCleanupModel>> strategies = new()
        {
            new CampCleanupPart1Strategy(),
            new CampCleanupPart2Strategy()
        };
        [Test]
        public void Part1_1()
        {
            var p = new CampCleanupService(strategies);
            p.SetStrategy("Part 1");
            var s = p.GetStepsToSolution(input1).Last();
            Assert.That(p.Solution, Is.EqualTo("2"));
        }
        [Test]
        public void Part2_1()
        {
            var p = new CampCleanupService(strategies);
            p.SetStrategy("Part 2");
            var s = p.GetStepsToSolution(input1).Last();
            Assert.That(p.Solution, Is.EqualTo("4"));
        }

        string input1 = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8";
    }
}
