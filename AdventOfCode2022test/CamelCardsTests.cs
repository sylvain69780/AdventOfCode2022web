using Domain.CamelCards;
namespace Tests
{
    internal class CamelCardsTests
    {
        List<IPuzzleStrategy<CamelCardsModel>> s = new()
        {
            new CamelCardsPart1Strategy(),
            new CamelCardsPart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new CamelCardsModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new CamelCardsService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("6440"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new CamelCardsService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("251927063"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new CamelCardsService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("5905"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new CamelCardsService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("255632664")); // low
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}CamelCards.txt");

        string input2 = File.ReadAllText($"{path}CamelCards_full.txt");
    }
}
