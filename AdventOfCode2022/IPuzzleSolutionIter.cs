using System.Globalization;
using System.Reflection;

namespace Domain
{

    public interface IPuzzleSolution
    {
        void Initialize(string puzzleInput);
        IEnumerable<string> SolveFirstPart();
        IEnumerable<string> SolveSecondPart();
    }
}
