
namespace Tests
{
    internal class DistressSignalTests
    {
        List<IPuzzleStrategy<DistressSignalModel>> s = new()
        {
            new DistressSignalPart1Strategy(),
            new DistressSignalPart2Strategy(),
            new DistressSignalPart1JsonStrategy(),
            new DistressSignalPart2JsonStrategy()
        };

        [Test]
        public void Part1_1()
        {
            var service = new DistressSignalService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("13"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new DistressSignalService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo(@"140"));
        }

        [Test]
        public void Part1Json_1()
        {
            var service = new DistressSignalService(s);
            service.SetStrategy("Part 1 using Json");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("13"));
        }

        [Test]
        public void Part2Json_1()
        {
            var service = new DistressSignalService(s);
            service.SetStrategy("Part 2 using Json");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo(@"140"));
        }

        string input = @"[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]".Replace("\r","");
    }
}
