using System.Globalization;
using System.Reflection;

namespace sylvain69780.AdventOfCode2022.Domain
{

    public interface IPuzzleSolution
    {
        void Initialize(string puzzleInput);
        IEnumerable<string> SolveFirstPart();
        IEnumerable<string> SolveSecondPart();
    }
}
