using Domain.PointOfIIcidence;
namespace Tests
{
    internal class PointOfIIcidenceTests
    {
        List<IPuzzleStrategy<PointOfIIcidenceModel>> s = new()
        {
            new PointOfIIcidencePart1Strategy(),
            new PointOfIIcidencePart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new PointOfIIcidenceModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new PointOfIIcidenceService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("405"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new PointOfIIcidenceService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("30802")); // low 
        }

        [Test]
        public void Part2_1()
        {
            var service = new PointOfIIcidenceService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("400"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new PointOfIIcidenceService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("37876")); // high 
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}PointOfIIcidence.txt");

        string input2 = File.ReadAllText($"{path}PointOfIIcidence_full.txt");
    }
}
