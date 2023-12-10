using Domain.PipeMaze;
namespace Tests
{
    internal class PipeMazeTests
    {
        List<IPuzzleStrategy<PipeMazeModel>> s = new()
        {
            new PipeMazePart1Strategy(),
            new PipeMazePart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new PipeMazeModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new PipeMazeService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("80"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new PipeMazeService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("6690"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new PipeMazeService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("10"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new PipeMazeService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("525")); 
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}PipeMaze.txt");

        string input2 = File.ReadAllText($"{path}PipeMaze_full.txt");
    }
}
