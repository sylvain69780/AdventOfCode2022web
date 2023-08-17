namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(7, "No Space Left On Device")]
    public class NoSpaceLeftOnDevice : IPuzzleSolver
    {
        private string _puzzleInput = string.Empty;
        public void Initialize(string puzzleInput)
        {
            _puzzleInput = puzzleInput;
        }
        private static string Format(int v) => v.ToString();
        private static string[] ToLines(string s) => s.Split("\n");

        private static Dictionary<string, int> BuildDirectoriesContentSize(string[] terminalOutputs)
        {
            var directoriesContentSize = new Dictionary<string, int>
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
                            currentDirectory.Pop();
                        else
                            currentDirectory.Push(directory);
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
                        foreach (var d in directoriesContentSize.Keys.Where(x => directory.Contains(x)))
                            directoriesContentSize[d] += size;
                    }
                    else
                    {
                        var directory = "#" + string.Join("-", currentDirectory.Reverse()) + "-" + terminalOutput[4..];
                        directoriesContentSize.Add(directory, 0);
                    }
                }
            }
            return directoriesContentSize;
        }

        public string SolveFirstPart()
        {
            var terminalOutputs = ToLines(_puzzleInput);
            var directoriesContentSize = BuildDirectoriesContentSize(terminalOutputs);
            var sumOfTotalSizesOfDirectories = directoriesContentSize.Values.Where(x => x <= 100000).Sum();
            return Format(sumOfTotalSizesOfDirectories);
        }
        public string SolveSecondPart()
        {
            var terminalOutputs = ToLines(_puzzleInput);
            var directoriesContentSize = BuildDirectoriesContentSize(terminalOutputs);
            var totalDiskSize = 70000000;
            var freeSpaceRequired = 30000000;
            var totalSpaceUsed = directoriesContentSize["#/"];
            var toBeFreed = freeSpaceRequired - (totalDiskSize - totalSpaceUsed);
            var totalSizeOfDirectoryToBeDeleted = directoriesContentSize.Values.Where(x => x >= toBeFreed).Min();
            return Format(totalSizeOfDirectoryToBeDeleted);
        }
    }
}