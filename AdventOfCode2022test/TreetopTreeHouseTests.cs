using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class TreetopTreeHouseTests
    {
        List<IPuzzleStrategy<TreetopTreeHouseModel>> s = new()
        {
            new TreetopTreeHousePart1Strategy(),
            new TreetopTreeHousePart2Strategy()
        };

        [Test]
        public void Part1_1()
        {
            var service = new TreetopTreeHouseService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("21"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new TreetopTreeHouseService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("8"));
        }

        string input = @"30373
25512
65332
33549
35390".Replace("\r","");
    }
}
