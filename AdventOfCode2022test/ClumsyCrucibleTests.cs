using Domain.ClumsyCrucible;
namespace Tests
{
    internal class ClumsyCrucibleTests
    {
        List<IPuzzleStrategy<ClumsyCrucibleModel>> s = new()
        {
            new ClumsyCruciblePart1Strategy(),
            new ClumsyCruciblePart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new ClumsyCrucibleModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new ClumsyCrucibleService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("102"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new ClumsyCrucibleService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("963"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new ClumsyCrucibleService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("94"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new ClumsyCrucibleService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("1178"));
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}ClumsyCrucible.txt");

        string input2 = File.ReadAllText($"{path}ClumsyCrucible_full.txt");
    }
}
