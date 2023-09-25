using System.Globalization;
using System.Reflection;

namespace AdventOfCode2022Solutions.PuzzleSolutions
{

    public interface IPuzzleSolutionIter
    {
        void Initialize(string puzzleInput);
        IEnumerable<string> SolveFirstPart();
        IEnumerable<string> SolveSecondPart();
    }
}
