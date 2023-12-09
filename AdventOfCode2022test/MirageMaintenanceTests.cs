using Domain.MirageMaintenance;
namespace Tests
{
    internal class MirageMaintenanceTests
    {
        List<IPuzzleStrategy<MirageMaintenanceModel>> s = new()
        {
            new MirageMaintenancePart1Strategy(),
            new MirageMaintenancePart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new MirageMaintenanceModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new MirageMaintenanceService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("114"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new MirageMaintenanceService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("1974913025"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new MirageMaintenanceService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("2"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new MirageMaintenanceService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("884")); 
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}MirageMaintenance.txt");

        string input2 = File.ReadAllText($"{path}MirageMaintenance_full.txt");
    }
}
