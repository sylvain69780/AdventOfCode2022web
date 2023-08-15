using AdventOfCode2022web.Puzzles;

namespace AdventOfCode2022test
{
    public class SudokuTest
    {
        Sudoku _sudoku = new Sudoku();
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void TestSudoku1()
        {
            var input = @"
132865.49
.9837..1.
..4.29..8
3.95.1.76
.7.2.6...
2.674...1
42....1..
..5......
.8.9...24";
            var solution = @"132865749598374612764129358349581276871296435256743981427658193915432867683917524";

            _sudoku.Setup(input);
            var res = _sudoku.SolveFirstPart().Last().Replace("\n", "");
            Assert.That(res, Is.EqualTo(solution));
        }
        [Test]
        public void TestSudoku2()
        {
            var input = @"
..81....4
1...8.2.7
.74..3...
.....7..6
3..2.4..5
7..3.....
...9..76.
8.6.4...9
2....81..";
            var solution = @"628179354153486297974523681542817936361294875789365412415932768836741529297658143";

            _sudoku.Setup(input);
            var algo = _sudoku.SolveFirstPart().ToArray();
            var steps = algo.Length;
            var res = algo[^1].Replace("\n", "");
            Assert.That(res, Is.EqualTo(solution));
        }
        [Test]
        public void TestSudoku3()
        {
            var input = @"
.6..529..
.79......
..2.9..5.
..48.....
21.....94
.....93..
.4..7.8..
......72.
..368..4.";
            var solution = @"461352987579168432832794156394816275218537694657429318945273861186945723723681549";

            _sudoku.Setup(input);
            var algo = _sudoku.SolveFirstPart().ToArray();
            var steps = algo.Length;
            var res = algo[^1].Replace("\n", "");
            Assert.That(res, Is.EqualTo(solution));
        }
    }
}
