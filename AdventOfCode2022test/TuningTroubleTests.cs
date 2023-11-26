using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class TuningTroubleTests
    {
        List<IPuzzleStrategy<TuningTroubleModel>> s = new()
        {
            new TuningTroublePart1Strategy(),
            new TuningTroublePart2Strategy()
        };

        [Test]
        public void Part1_1()
        {
            var service = new TuningTroubleService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("11"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new TuningTroubleService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("26"));
        }

        string input = @"zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";
    }
}
