namespace AdventOfCode2022web.Domain.Puzzle
{
    public class NoSpaceLeftOnDevice : IPuzzleSolver
    {
        private static string Format(int v) => v.ToString();
        private static string[] ToLines(string s) => s.Split("\n");

        public IEnumerable<string> SolveFirstPart(string puzzleInput)
        {
            var terminalOutputs = ToLines(puzzleInput);
            var directoryContentSize = new Dictionary<string, int>
            {
                { "#/", 0 }
            };
            var currentDirectory = new Stack<string>();
            foreach (var terminalOutput in terminalOutputs)
            {
                if (terminalOutput[0] == '$')
                {
                    if (terminalOutput[2..4] == "cd")
                    {
                        var directory = terminalOutput[5..];
                        if (directory == "..")
                        {
                            currentDirectory.Pop();
                        }
                        else currentDirectory.Push(directory);
                        Console.WriteLine("#" + string.Join("-", currentDirectory.Reverse()));
                    }
                }
                else
                {
                    if (terminalOutput[0..4] != "dir ")
                    {
                        var directory = "#" + string.Join("-", currentDirectory.Reverse());
                        var size = int.Parse(terminalOutput.Split(" ")[0]);
                        // tricky here we add also to parents
                        foreach (var d in directoryContentSize.Keys.Where(x => directory.Contains(x)))
                            directoryContentSize[d] += size;
                    }
                    else
                    {
                        var directory = "#" + string.Join("-", currentDirectory.Reverse()) + "-" + terminalOutput[4..];
                        directoryContentSize.Add(directory, 0);
                    }
                }
            }
            var sumOfTotalSizesOfDirectories = directoryContentSize.Values.Where(x => x <= 100000).Sum();
            yield return Format(sumOfTotalSizesOfDirectories);
        }
        public IEnumerable<string> SolveSecondPart(string puzzleInput)
        {
            var terminalOutputs = ToLines(puzzleInput);
            var directoryContentSize = new Dictionary<string, int>
            {
                { "#/", 0 }
            };
            var currentDirectory = new Stack<string>();
            foreach (var terminalOutput in terminalOutputs)
            {
                if (terminalOutput[0] == '$')
                {
                    if (terminalOutput[2..4] == "cd")
                    {
                        var directory = terminalOutput[5..];
                        if (directory == "..")
                        {
                            currentDirectory.Pop();
                        }
                        else currentDirectory.Push(directory);
                        Console.WriteLine("#" + string.Join("-", currentDirectory.Reverse()));
                    }
                }
                else
                {
                    if (terminalOutput[0..4] != "dir ")
                    {
                        var directory = "#" + string.Join("-", currentDirectory.Reverse());
                        var size = int.Parse(terminalOutput.Split(" ")[0]);
                        // tricky here we add also to parents
                        foreach (var d in directoryContentSize.Keys.Where(x => directory.Contains(x)))
                            directoryContentSize[d] += size;
                    }
                    else
                    {
                        var directory = "#" + string.Join("-", currentDirectory.Reverse()) + "-" + terminalOutput[4..];
                        directoryContentSize.Add(directory, 0);
                    }
                }
            }
            var totalDiskSize = 70000000;
            var freeSpaceRequired = 30000000;
            var totalSpaceUsed = directoryContentSize["#/"];
            var toBeFreed = freeSpaceRequired - (totalDiskSize - totalSpaceUsed);
            var totalSizeOfDirectoryToBeDeleted = directoryContentSize.Values.Where(x => x >= toBeFreed).Min();
            yield return Format(totalSizeOfDirectoryToBeDeleted);
        }
    }
}