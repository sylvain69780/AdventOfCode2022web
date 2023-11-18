﻿namespace sylvain69780.AdventOfCode2022.Tests
{
    public class UnstableDiffusionTests
    {
        private IPuzzleSolution _solver;
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
            input2 = @"#.....###.....#.###....#.#..#...###..#..#....#.#.......#.##.###.####.#...#
##.#..#.#.#.#..##.##......####.##.##...#...##..#.....####..#.#..###...#..#
.#.###...##.#.###.#..##.#..#.###..#.###..#..##.##.########....#...#...##..
##.#...#.##..#...############.#..#..##.#.#.#.####....#.##....#...##.####..
##.#.###....#.##.##.#.####....####.###.#..#...#####.#..###.#.###.....#.#..
.#..#####.#...###..#..#..###..#.#...#.###.###.#.##.#.....#.#.##.##.#.....#
#.#...##..#.###.##.##.##........#.#.##....#..##...#..#####..##..#....#.##.
###....###..#..#.....#..####...###.#.#....#.#.#.#..##.###.#..#..######....
#..#..#...#.#.#.#....#.#.##.#.#.#.#######..#.##...##..####.###.##.#.###.#.
###.##...#...##..#...#.##.##.##.#.#.#.#..#.#.#...###....#.#.##.....###....
....##...##.#.##.#..#...#..#...#.##.#...##..#...#.##...##..#.#.##..#.#....
##.....#.##....##...###.#.#####...####.###....#######.#.....##.#.#..#.##..
.......#####..#.###.#...###..##.##..#.#.###...#..#..##..##.#..#.#..#..##..
#.#..###.#.#.#..#####....###.#..#.#.###...##....#.#.####.##...#....#.#.#..
..#.....##.#.#..###.#...######.##....#####.###...####.#####.##..#...###.#.
.#.####.......##.###.#..#####.#######...##.#.###..#......##..#...#.#.#.#..
##.###.....#.##..#.#.#.#.#..##....####.#...#.###...#.####..#.#......#..#.#
####.......##....######.#.#.#.#..#.##.###.....#...#.#..#.#.###..#.###..##.
##..###..###...##...#.#..##.#..#.#.#.#...#..##..#.#..#....#...##...##.###.
#....#.....####..##.#........#.#..#...#...##...##.#..#..#.#.#.#..#......##
#.#.#..#..##.####......#..#####.#.##..##..##...####.###..#####...#.#..##.#
####.#.##.....#..####.##.####.#####...#####.#....##....#..#..#.###.#.##.#.
..#.##.##.##.#.#...#.###..#.##.######..#..#...#.###..###.###.#.#.#..#.#.#.
..##..##..#..#####...##.###...###.###########.#..#######..#.#.#....#######
#.###.#.##..######.###..##...#..##.#.###..##..#..#.#.#..#.##.#.#..#.#.####
#....#.##...##..#.#.####.......#.#.###.#......###......#..##...#..#.####..
##.####..######.####....#.###.##..#.#.##.#.#..##.#..##.#.##.#.##.#####..#.
#######.####.#.##.########..#..###.###.###...##......#..##.#.#####...#...#
#..##.#.#..######.##.##..#....#..##.#.#####.##..#.##.##..#..###.#.##..##.#
..#.#.#..##...####....##.#.#..####..#.#####.###.#..##.....#..##..##.#.#..#
#.###..####....####..###.#.####.#.##....#.##.###.##.###########..#..###.##
..##.##.##.#.####......####...##..#....#####...#.#....#####..#.#####..##.#
#..##...#...###.#..#.##.##.#.#.#....#..##.###.###.##........#..####.##...#
...#####.####..###........#.##...##....#...#..#...##..###..#####...######.
..#..##..#.......##.#....###..######.##.####.##..##.....##.###.#.#####.#.#
..#.#...#.......##.##.###..#.#.#...#########..###...#....#...#.###.#..#.##
#.#..#..######..#........##..###.##..#.##.#.#..#.#....##.#.#..#.#...#.#.#.
#....#.#.#####.###......#..#..##.##...#......##..#.###..##.####.######.#..
#..#.....#..####.#...##.####..######.#.#.###..##.....##.#.##.######.#.####
....###.#######..#.##..########.....#.#..##..#.#..#.##....#.#..#..#..##.#.
#..#.##.#.#.#.##.#.#.....#..#..#...#..##..##.#..#.##.#.#.#...##.##.#...#.#
.#.######.#..##.#.##.....##.#.##.#.#....######..##..##.#...#...#.#.###..##
.#.#..#..#..#..##.#.####.#.####.#....#.#.###.###.#..###..##.#...#.###.###.
##.#.##..###.####.#.#.###.#......#####.###.######.#..######.#...#.#.####..
###.####..#.###....#.....#.#.##.#.##.#####...##.#...#####.#.###.#..##.....
##..#.#..#.####.#.####...###.#.##..#####.#..###...#####.####..#...#.##.#.#
..##.#####..##.#####..#..#..#..#...##..#.###..##.#....##...#...#.#.##...##
##...#.##..#.##...#.#.####..#.#...####..#.##.#.#...#####.##..#######.##...
.#..####.##...#.#...##..#..###.#####.#.#.#.#..##..##.###.....#...#..####..
.##..#...#.###.####.#.#...#...#####..#..#.#.##...#...##..#.#..#.#.#.###..#
###.##.####.##....#.#..#.##.##..###.####..##..#.####.....#.#.###..#.######
#.##.#.##.#..#..#.#......##.#.##.#..#.#.#.....###.....#......###....#..###
.#..#..#....#..#.#...#..#..##......##...###.###.##...#...#.....#...###.###
#..##..#..##.#....###...#......#.######..#.###..#.#....#......#.##..#.####
.#...##.#..#####..##...#####.#....#######....#.....###....#..###.##..#..#.
.###.#.##..#..#..##..#....#...#.##.##.##.#..#...##..#..##...##..######.###
#....#.#####.###..#.#.......##..##...##..#.###..#####.#..#..##.#..#....##.
##..#......#######...#.##.###..#.....##..#.####.###.##.#..####.#.#####.##.
..#....###.#........#..######.#...##.......##.#.###.##.#...#.##....##.#...
###.....#..##.####.#..###..#..#####.##.##.#######.#.####.####......#.##.##
..#...#...#######.#.####....#.####..#..#..##.#.#.###..#.#####.##.#.....#.#
###.#.#..#.####.##...#.#####.##.####..#.#.###.#..#...#..#########.###.#.##
..######.########..###.#.#.###.#.#.#.#.####.#...#..##...##.#####..##...#.#
...#.#...##.#....####.###..#..#..#..##..###.##..#.#....#..#...######.##.##
#####.##..#..##..#####.#..#..#..###.##..##...###....#.###.#.#.##...###..##
..#.##.#....####..#.#..###.#.#..##...#..##....####.............#.#.#..#.##
.###...#.#....#..#.######....###.....#..#...##..##.#....##...#.###....###.
.#.####..#.#.###..###.#..###.#..#..#######....###.......#....###.#...#.###
###.#.###.#..#.#..###.#.....#.##..#.#.####..###...#...#####...####...##...
.#..##..#.##..#..##..######.###.##.....##..#..#.##...####.#...#####..###.#
..##...######.#.#...#.#.#####.##.##...##.##....#.#####...###....#.#.#.#..#
##..#.#.#....##.####.#.##.##.#.#.##......#...##..##...##.#####...#..#..##.
...####.#..###....#.###.#.#.........#....####..#........#.###..####.##...#
.##.##.#####...##.....##.#..#####..##.....#....##......#.#..###..###..##..";

            _solver = new UnstableDiffusionSolution();
        }

        [Test]
        public void TestFirstPart0()
        {
            _solver.Initialize(input0);
            Assert.That(_solver.SolveFirstPart().Last(), Is.EqualTo("25"));
        }
        [Test]
        public void TestFirstPart()
        {
            _solver.Initialize(input1);
            Assert.That(_solver.SolveFirstPart().Last(), Is.EqualTo("110"));
        }
        [Test]
        public void TestFirstPartFull()
        {
            _solver.Initialize(input2);
            Assert.That(_solver.SolveFirstPart().Last(), Is.EqualTo("4116"));
        }
        [Test]
        public void TestSecondPart()
        {
            _solver.Initialize(input1);
            Assert.That(_solver.SolveSecondPart().Last(), Is.EqualTo("20"));
        }
        [Test]
        public void TestSecondPartFull()
        {
            _solver.Initialize(input2);
            Assert.That(_solver.SolveSecondPart().Last(), Is.EqualTo("984"));
        }
    }
}