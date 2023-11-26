using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class RopeBridgeTests
    {
        List<IPuzzleStrategy<RopeBridgeModel>> s = new()
        {
            new RopeBridgePart1Strategy(),
            new RopeBridgePart2Strategy()
        };

        [Test]
        public void Part1_1()
        {
            var service = new RopeBridgeService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("13"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new RopeBridgeService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("1"));
        }

        string input = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2".Replace("\r","");
    }
}
