using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class MonkeyMathTests
    {
        List<IPuzzleStrategy<MonkeyMathModel>> s = new()
        {
            new MonkeyMathPart1Strategy(),
            new MonkeyMathPart2Strategy()
        };

        [Test]
        public void Part1_1()
        {
            var service = new MonkeyMathService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("152"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new MonkeyMathService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo(@"301"));
        }

        string input = @"root: pppw + sjmn
dbpl: 5
cczh: sllz + lgvd
zczc: 2
ptdq: humn - dvpt
dvpt: 3
lfqf: 4
humn: 5
ljgn: 2
sjmn: drzm * dbpl
sllz: 4
pppw: cczh / lfqf
lgvd: ljgn * ptdq
drzm: hmdt - zczc
hmdt: 32".Replace("\r","");
    }
}
