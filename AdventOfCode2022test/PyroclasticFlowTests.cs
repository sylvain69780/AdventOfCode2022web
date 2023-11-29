using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class PyroclasticFlowTests
    {
        List<IPuzzleStrategy<PyroclasticFlowModel>> s = new()
        {
            new PyroclasticFlowPart1Strategy(),
            new PyroclasticFlowPart2Strategy()
        };

        [Test]
        public void Part1_1()
        {
            var service = new PyroclasticFlowService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("3068"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new PyroclasticFlowService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo(@"1514285714288"));
        }

        string input = @">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>".Replace("\r","");
    }
}
