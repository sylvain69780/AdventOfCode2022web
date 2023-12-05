using Domain.Scratchcards;
namespace Tests
{
    internal class ScratchcardsTests
    {
        List<IPuzzleStrategy<ScratchcardsModel>> s = new()
        {
            new ScratchcardsPart1Strategy(),
            new ScratchcardsPart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new ScratchcardsModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new ScratchcardsService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("13"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new ScratchcardsService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("24733"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new ScratchcardsService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("30"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new ScratchcardsService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("5422730"));
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}Scratchcards.txt");

        string input2 = File.ReadAllText($"{path}Scratchcards_full.txt");
    }
}
