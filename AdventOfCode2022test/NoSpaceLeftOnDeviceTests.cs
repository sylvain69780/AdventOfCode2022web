using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class NoSpaceLeftOnDeviceTests
    {
        readonly List<IPuzzleStrategy<NoSpaceLeftOnDeviceModel>> s = new()
        {
            new NoSpaceLeftOnDevicePart1Strategy(),
            new NoSpaceLeftOnDevicePart2Strategy()
        };

        [Test]
        public void Part1_1()
        {
            var service = new NoSpaceLeftOnDeviceService(s);
            service.SetStrategy("Part 1");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("95437"));
        }

        [Test]
        public void Part2_1()
        {
            var service = new NoSpaceLeftOnDeviceService(s);
            service.SetStrategy("Part 2");
            var c = service.GetStepsToSolution(input).Count();
            Assert.That(service.Solution, Is.EqualTo("24933642"));
        }

        string input = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";
    }
}
