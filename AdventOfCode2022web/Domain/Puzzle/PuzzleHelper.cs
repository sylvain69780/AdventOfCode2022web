using System.Reflection;

namespace AdventOfCode2022web.Domain.Puzzle
{
    public interface IPuzzleSolver
    {
        IEnumerable<string> SolveFirstPart(string input);
        IEnumerable<string> SolveSecondPart(string input);
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PuzzleAttribute : Attribute
    {
        public int Number { get; private set; }
        public string Title { get; private set; }
        public PuzzleAttribute(int number,string title) => (Number,Title) = (number,title);
    }

    public static class PuzzleHelper
    {
        public static readonly IReadOnlyDictionary<int, (Type Type, int Number, string Title)> Puzzles = Assembly.GetExecutingAssembly().GetTypes()
        .Where(x => x.IsClass && typeof(IPuzzleSolver).IsAssignableFrom(x))
        .Select(x => (Type: x, Attr: x.GetCustomAttribute<PuzzleAttribute>()!))
        .Select(x => (x.Type, x.Attr.Number, x.Attr.Title))
        .OrderBy(x => x.Number).ToDictionary(x => x.Number);
    }
}
