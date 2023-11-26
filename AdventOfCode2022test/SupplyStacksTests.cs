using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class SupplyStacksTests
    {
        List<IPuzzleStrategy<SupplyStacksModel>> s = new()
        {
            new SupplyStacksPart1Strategy(),
            new SupplyStacksPart2Strategy()
        };

        [Test]
        public void Part1_1()
        {
            var service = new SupplyStacksService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("CMZ"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new SupplyStacksService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("MCD"));
        }

        string input = @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";
    }
}
