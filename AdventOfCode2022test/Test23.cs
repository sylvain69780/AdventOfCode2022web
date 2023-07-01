using AdventOfCode2022web.Puzzles;

namespace AdventOfCode2022test
{
    public class Test23
    {
        private IPuzzleSolverV3 _solver;
        string input1;
        string input0;
        string input2;
        [SetUp]
        public void Setup()
        {
            input1 = @"....#..
..###.#
#...#.#
.#...##
#.###..
##.#.##
.#..#..";
            input0 = @".....
..##.
..#..
.....
..##.
.....";
            _solver = new UnstableDiffusion();
        }

        [Test]
        public void TestFirstPart0()
        {
            _solver.Setup(input0);
            Assert.That(_solver.SolveFirstPart().Last(), Is.EqualTo("25"));
        }
        [Test]
        public void TestFirstPart()
        {
            _solver.Setup(input1);
            Assert.That(_solver.SolveFirstPart().Last(), Is.EqualTo("110"));
        }
        [Test]
        public void TestSecondPart()
        {
            _solver.Setup(input1);
            Assert.That(_solver.SolveSecondPart().Last(), Is.EqualTo("20"));
        }
    }
}