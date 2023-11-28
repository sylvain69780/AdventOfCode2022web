namespace Tests
{
    internal class RegolithReservoirTests
    {
        List<IPuzzleStrategy<RegolithReservoirModel>> s = new()
        {
            new RegolithReservoirPart1Strategy(),
            new RegolithReservoirPart2Strategy()
        };

        [Test]
        public void Part1_1()
        {
            var service = new RegolithReservoirService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("24"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new RegolithReservoirService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo(@"93"));
        }

        string input = @"498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9".Replace("\r","");
    }
}
