using Domain.BlizzardBasin;
namespace Tests
{
    internal class BlizzardBasinTests
    {
        List<IPuzzleStrategy<BlizzardBasinModel>> s = new()
        {
            new BlizzardBasinPart1Strategy(),
            new BlizzardBasinPart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new BlizzardBasinModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new BlizzardBasinService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("35"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new BlizzardBasinService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("175622908"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new BlizzardBasinService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("46"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new BlizzardBasinService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("5200543"));
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}BlizzardBasin.txt");

        string input2 = File.ReadAllText($"{path}BlizzardBasin_full.txt");
    }
}
