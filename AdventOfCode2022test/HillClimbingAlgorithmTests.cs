using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class HillClimbingAlgorithmTests
    {
        List<IPuzzleStrategy<HillClimbingAlgorithmModel>> s = new()
        {
            new HillClimbingAlgorithmPart1Strategy(),
            new HillClimbingAlgorithmPart2Strategy()
        };

        [Test]
        public void Part1_1()
        {
            var service = new HillClimbingAlgorithmService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("31"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new HillClimbingAlgorithmService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo(@"29"));
        }

        string input = @"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi".Replace("\r","");
    }
}
