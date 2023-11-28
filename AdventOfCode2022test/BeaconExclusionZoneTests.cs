using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class BeaconExclusionZoneTests
    {
        readonly List<IPuzzleStrategy<BeaconExclusionZoneModel>> s = new()
        {
            new BeaconExclusionZonePart1Strategy(),
            new BeaconExclusionZonePart2Strategy()
        };

        [Test]
        public void Part1_1()
        {
            var service = new BeaconExclusionZoneService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("26"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new BeaconExclusionZoneService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo(@"56000011"));
        }

        string input = @"Sensor at x=2, y=18: closest beacon is at x=-2, y=15
Sensor at x=9, y=16: closest beacon is at x=10, y=16
Sensor at x=13, y=2: closest beacon is at x=15, y=3
Sensor at x=12, y=14: closest beacon is at x=10, y=16
Sensor at x=10, y=20: closest beacon is at x=10, y=16
Sensor at x=14, y=17: closest beacon is at x=10, y=16
Sensor at x=8, y=7: closest beacon is at x=2, y=10
Sensor at x=2, y=0: closest beacon is at x=2, y=10
Sensor at x=0, y=11: closest beacon is at x=2, y=10
Sensor at x=20, y=14: closest beacon is at x=25, y=17
Sensor at x=17, y=20: closest beacon is at x=21, y=22
Sensor at x=16, y=7: closest beacon is at x=15, y=3
Sensor at x=14, y=3: closest beacon is at x=15, y=3
Sensor at x=20, y=1: closest beacon is at x=15, y=3".Replace("\r","");
    }
}
