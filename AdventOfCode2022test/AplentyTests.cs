using Domain.Aplenty;
namespace Tests
{
    internal class AplentyTests
    {
        List<IPuzzleStrategy<AplentyModel>> s = new()
        {
            new AplentyPart1Strategy(),
            new AplentyPart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new AplentyModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new AplentyService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("19114"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new AplentyService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("348378"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new AplentyService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("167409079868000"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new AplentyService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("121158073425385"));
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}Aplenty.txt");

        string input2 = File.ReadAllText($"{path}Aplenty_full.txt");
    }
}
