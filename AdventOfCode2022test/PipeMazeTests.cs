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
            var c = service.GetStepsToSolution(@"FF7FSF7F7F7F7F7F---7
L|LJ||||||||||||F--J
FL-7LJLJ||||||LJL-77
F--JF--7||LJLJ7F7FJ-
L---JF-JLJ.||-FJLJJ7
|F|F-JF---7F7-L7L|7|
|FFJF7L7F-JF7|JL---7
7-L-JL7||F7|L7F-7F7|
L.L7LFJ|||||FJL7||LJ
L7JLJL-JLJLJL--JLJ.L").Count();
            Assert.That(service.Solution, Is.EqualTo("10"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new PipeMazeService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("1686")); // high
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}PipeMaze.txt");

        string input2 = File.ReadAllText($"{path}PipeMaze_full.txt");
    }
}
