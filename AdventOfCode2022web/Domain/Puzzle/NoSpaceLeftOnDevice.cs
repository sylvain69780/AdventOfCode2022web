namespace AdventOfCode2022web.Domain.Puzzle
{
    public class NoSpaceLeftOnDevice : PuzzleSolver
    {
        protected override string Part1(string inp)
        {
            var input = inp.Split("\n");
            var contents = new Dictionary<string, int>
            {
                { "#/", 0 }
            };
            var curdir = new Stack<string>();
            foreach (var line in input)
            {
                if (line[0] == '$')
                {
                    if (line[2..4] == "cd")
                    {
                        var dir = line[5..];
                        if (dir == "..")
                        {
                            curdir.Pop();
                        }
                        else curdir.Push(dir);
                        Console.WriteLine("#" + string.Join("-", curdir.Reverse()));
                    }
                }
                else
                {
                    if (line[0..4] != "dir ")
                    {
                        var dir = "#" + string.Join("-", curdir.Reverse());
                        var size = int.Parse(line.Split(" ")[0]);
                        // tricky here we add also to parents
                        foreach (var d in contents.Keys.Where(x => dir.Contains(x)))
                            contents[d] += size;
                    }
                    else
                    {
                        var dir = "#" + string.Join("-", curdir.Reverse()) + "-" + line[4..];
                        contents.Add(dir, 0);
                    }
                }
            }
            var atMost = 100000;
            var res = contents.Values.Where(x => x <= atMost).Sum();
            Console.WriteLine("Result :" + res);
            return res.ToString();
        }
        protected override string Part2(string inp)
        {
            var input = inp.Split("\n");
            var contents = new Dictionary<string, int>
            {
                { "#/", 0 }
            };
            var curdir = new Stack<string>();
            foreach (var line in input)
            {
                if (line[0] == '$')
                {
                    if (line[2..4] == "cd")
                    {
                        var dir = line[5..];
                        if (dir == "..")
                        {
                            curdir.Pop();
                        }
                        else curdir.Push(dir);
                        Console.WriteLine("#" + string.Join("-", curdir.Reverse()));
                    }
                }
                else
                {
                    if (line[0..4] != "dir ")
                    {
                        var dir = "#" + string.Join("-", curdir.Reverse());
                        var size = int.Parse(line.Split(" ")[0]);
                        // tricky here we add also to parents
                        foreach (var d in contents.Keys.Where(x => dir.Contains(x)))
                            contents[d] += size;
                    }
                    else
                    {
                        var dir = "#" + string.Join("-", curdir.Reverse()) + "-" + line[4..];
                        contents.Add(dir, 0);
                    }
                }
            }
            var total = 70000000;
            var freeRequired = 30000000;
            var used = contents["#/"];
            var toBeFreed = freeRequired - (total - used);
            var res = contents.Values.Where(x => x >= toBeFreed).Min();
            Console.WriteLine("Result :" + res);
            return res.ToString();
        }
    }
}