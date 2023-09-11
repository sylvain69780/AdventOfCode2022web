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

    [AttributeUsage(AttributeTargets.Class)]
    public class PuzzleAttribute : Attribute
    {
        public int Number { get; private set; }
        public string Title { get; private set; }
        public bool HasVisualization;
        public PuzzleAttribute(int number, string title,bool hasVisualization = false) => (Number, Title,HasVisualization) = (number, title,hasVisualization);
    }

    public class PuzzleHelper
    {
        public readonly IReadOnlyDictionary<int, (Type Type, int Number, string Title,bool HasVisualization)> Puzzles = Assembly.GetExecutingAssembly().GetTypes()
        .Where(x => x.IsClass && !x.IsGenericType 
        && typeof(IPuzzleSolutionIter).IsAssignableFrom(x)
        && x.IsDefined(typeof(PuzzleAttribute)))
        .Select(x => (Type: x, Attr: x.GetCustomAttribute<PuzzleAttribute>()!))
        .Select(x => (x.Type, x.Attr.Number, x.Attr.Title, x.Attr.HasVisualization))
        .ToDictionary(x => x.Number);
    }
}
