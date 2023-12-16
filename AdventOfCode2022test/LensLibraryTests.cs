using Domain.LensLibrary;
namespace Tests
{
    internal class LensLibraryTests
    {
        List<IPuzzleStrategy<LensLibraryModel>> s = new()
        {
            new LensLibraryPart1Strategy(),
            new LensLibraryPart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new LensLibraryModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new LensLibraryService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("1320"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new LensLibraryService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("516804"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new LensLibraryService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("145"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new LensLibraryService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("231844"));
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}LensLibrary.txt");

        string input2 = File.ReadAllText($"{path}LensLibrary_full.txt");
    }
}
