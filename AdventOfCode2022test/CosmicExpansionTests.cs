using Domain.CosmicExpansion;
namespace Tests
{
    internal class CosmicExpansionTests
    {
        List<IPuzzleStrategy<CosmicExpansionModel>> s = new()
        {
            new CosmicExpansionPart1Strategy(),
            new CosmicExpansionPart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new CosmicExpansionModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new CosmicExpansionService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("374"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new CosmicExpansionService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("9556896"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new CosmicExpansionService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("82000210"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new CosmicExpansionService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("685038186836"));
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}CosmicExpansion.txt");

        string input2 = File.ReadAllText($"{path}CosmicExpansion_full.txt");
    }
}
