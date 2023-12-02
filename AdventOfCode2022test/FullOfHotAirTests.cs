using Domain.FullOfHotAir;
namespace Tests
{
    internal class FullOfHotAirTests
    {
        List<IPuzzleStrategy<FullOfHotAirModel>> s = new()
        {
            new FullOfHotAirPart1Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new FullOfHotAirModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new FullOfHotAirService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("2=-1=0"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new FullOfHotAirService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("2-212-2---=00-1--102"));
        }

        //        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}FullOfHotAir.txt");

        string input2 = File.ReadAllText($"{path}FullOfHotAir_full.txt");
    }
}
