using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class MonkeyInTheMiddleTests
    {
        List<IPuzzleStrategy<MonkeyInTheMiddleModel>> s = new()
        {
            new MonkeyInTheMiddlePart1Strategy(),
            new MonkeyInTheMiddlePart2Strategy()
        };

        [Test]
        public void Part1_1()
        {
            var service = new MonkeyInTheMiddleService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("10605"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new MonkeyInTheMiddleService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo(@"2713310158"));
        }

        string input = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1".Replace("\r","");
    }
}
