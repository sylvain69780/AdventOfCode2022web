using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NoSpaceLeftOnDevice
{
    public class NoSpaceLeftOnDeviceModel : IPuzzleModel
    {
        public void Parse(string input)
        {
            _puzzleInput = input;
        }

        private string _puzzleInput = string.Empty;

        public Dictionary<string, int> BuildDirectoriesContentSize()
        {
            var terminalOutputs = _puzzleInput.Replace("\r","").Split("\n");
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

    }
}
