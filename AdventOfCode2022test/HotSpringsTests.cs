using Domain.HotSprings;
namespace Tests
{
    internal class HotSpringsTests
    {
        List<IPuzzleStrategy<HotSpringsModel>> s = new()
        {
            new HotSpringsPart1Strategy(),
            new HotSpringsPart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new HotSpringsModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new HotSpringsService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("21"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new HotSpringsService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("7110"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new HotSpringsService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("525152"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new HotSpringsService(s);
            service.SetStrategy("Part 2");
            TestContext.Progress.WriteLine("Start");
            foreach (var c in service.GetStepsToSolution(input2))
            {
                TestContext.Progress.WriteLine($"Step {c.Step}");
            };
            Assert.That(service.Solution, Is.EqualTo("1566786613613")); // low
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}HotSprings.txt");

        string input2 = File.ReadAllText($"{path}HotSprings_full.txt");
    }
}
