using Domain.CubeConundrum;
namespace Tests
{
    internal class CubeConundrumTests
    {
        List<IPuzzleStrategy<CubeConundrumModel>> s = new()
        {
            new CubeConundrumPart1Strategy(),
            new CubeConundrumPart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new CubeConundrumModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new CubeConundrumService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("8"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new CubeConundrumService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("2879"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new CubeConundrumService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("2286"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new CubeConundrumService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("65122"));
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}CubeConundrum.txt");

        string input2 = File.ReadAllText($"{path}CubeConundrum_full.txt");
    }
}
