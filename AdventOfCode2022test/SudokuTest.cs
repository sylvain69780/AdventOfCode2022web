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
            var res = _sudoku.SolveFirstPart().Last();
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
            var res = algo[^1];
            Assert.That(res, Is.EqualTo(solution));
        }
    }
}
