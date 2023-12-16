using Domain.TheFloorWillBeLava;
namespace Tests
{
    internal class TheFloorWillBeLavaTests
    {
        List<IPuzzleStrategy<TheFloorWillBeLavaModel>> s = new()
        {
            new TheFloorWillBeLavaPart1Strategy(),
            new TheFloorWillBeLavaPart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new TheFloorWillBeLavaModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new TheFloorWillBeLavaService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("46"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new TheFloorWillBeLavaService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("7728"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new TheFloorWillBeLavaService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("51"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new TheFloorWillBeLavaService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("8061"));
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}TheFloorWillBeLava.txt");

        string input2 = File.ReadAllText($"{path}TheFloorWillBeLava_full.txt");
    }
}
