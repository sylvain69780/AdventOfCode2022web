using Domain.WaitForIt;
namespace Tests
{
    internal class WaitForItTests
    {
        List<IPuzzleStrategy<WaitForItModel>> s = new()
        {
            new WaitForItPart1Strategy(),
            new WaitForItPart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new WaitForItModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new WaitForItService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("288"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new WaitForItService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("1159152"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new WaitForItService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("71503"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new WaitForItService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("41513103"));
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}WaitForIt.txt");

        string input2 = File.ReadAllText($"{path}WaitForIt_full.txt");
    }
}
