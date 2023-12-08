using Domain.HauntedWasteland;
namespace Tests
{
    internal class HauntedWastelandTests
    {
        List<IPuzzleStrategy<HauntedWastelandModel>> s = new()
        {
            new HauntedWastelandPart1Strategy(),
            new HauntedWastelandPart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new HauntedWastelandModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new HauntedWastelandService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("2"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new HauntedWastelandService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("19667"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new HauntedWastelandService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(@"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)").Count();
            Assert.That(service.Solution, Is.EqualTo("6"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new HauntedWastelandService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("255632664")); // low
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}HauntedWasteland.txt");

        string input2 = File.ReadAllText($"{path}HauntedWasteland_full.txt");
    }
}
