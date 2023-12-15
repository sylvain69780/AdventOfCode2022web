using Domain.ParabolicReflectorDish;
namespace Tests
{
    internal class ParabolicReflectorDishTests
    {
        List<IPuzzleStrategy<ParabolicReflectorDishModel>> s = new()
        {
            new ParabolicReflectorDishPart1Strategy(),
            new ParabolicReflectorDishPart2Strategy()
        };

        [Test]
        public void ParseInput()
        {
            var m = new ParabolicReflectorDishModel();
            m.Parse(input);
        }
        [Test]
        public void Part1_1()
        {
            var service = new ParabolicReflectorDishService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("136"));
        }

        [Test]
        public void Part1_2()
        {
            var service = new ParabolicReflectorDishService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("108759"));
        }

        //[Test]
        public void Part2_1()
        {
            var service = new ParabolicReflectorDishService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("64"));
        }

        [Test]
        public void Part2_2()
        {
            var service = new ParabolicReflectorDishService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input2).Count();
            Assert.That(service.Solution, Is.EqualTo("89089")); 
        }

//        const string path = "C:\\Users\\sylvain.lecourtois\\source\\repos\\dev\\AdventOfCode2022web\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        const string path = "..\\..\\..\\..\\AdventOfCode2022web\\wwwroot\\sample-data\\";
        string input = File.ReadAllText($"{path}ParabolicReflectorDish.txt");

        string input2 = File.ReadAllText($"{path}ParabolicReflectorDish_full.txt");
    }
}
