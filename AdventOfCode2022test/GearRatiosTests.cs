using Domain.GearRatios;
namespace Tests
{
    internal class GearRatiosTests
    {
        List<IPuzzleStrategy<GearRatiosModel>> s = new()
        {
            new GearRatiosPart1Strategy(),
            new GearRatiosPart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new GearRatiosModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new GearRatiosService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("4361"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new GearRatiosService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("535351"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new GearRatiosService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("467835"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new GearRatiosService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("87287096"));
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}GearRatios.txt");

        string input2 = File.ReadAllText($"{path}GearRatios_full.txt");
    }
}
