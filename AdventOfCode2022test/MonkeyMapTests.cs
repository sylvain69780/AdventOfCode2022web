using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class MonkeyMapTests
    {
        List<IPuzzleStrategy<MonkeyMapModel>> s = new()
        {
            new MonkeyMapPart1Strategy(),
            new MonkeyMapPart2Strategy()
        };

        [Test]
        public void Part1_1()
        {
            var service = new MonkeyMapService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("6032"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new MonkeyMapService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo(@"5031"));
        }

        string input = @"        ...#
        .#..
        #...
        ....
...#.......#
........#...
..#....#....
..........#.
        ...#....
        .....#..
        .#......
        ......#.

10R5L5R10L4R5L5".Replace("\r","");
    }
}
