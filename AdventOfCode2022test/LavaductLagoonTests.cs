using Domain.LavaductLagoon;
namespace Tests
{
    internal class LavaductLagoonTests
    {
        List<IPuzzleStrategy<LavaductLagoonModel>> s = new()
        {
            new LavaductLagoonPart1Strategy(),
            new LavaductLagoonPart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new LavaductLagoonModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new LavaductLagoonService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("62"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new LavaductLagoonService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("45159")); 
        }

        [Test]
        public void Part2_1()
        {
            var service = new LavaductLagoonService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("952408144115"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new LavaductLagoonService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("134549294799713"));
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}LavaductLagoon.txt");

        string input2 = File.ReadAllText($"{path}LavaductLagoon_full.txt");
    }
}
