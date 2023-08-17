using System.Globalization;
using System.Reflection;

namespace AdventOfCode2022web.Puzzles
{
    public interface IPuzzleSolver
    {
        void Initialize(string input);
        string SolveFirstPart();
        string SolveSecondPart();
    }

    public interface IIncrementalPuzzleSolver
    {
        void Initialize(string puzzleInput);
        IEnumerable<string> SolveFirstPart();
        IEnumerable<string> SolveSecondPart();
    }

    public class PuzzleSolverWrapper<T> : IIncrementalPuzzleSolver where T:IPuzzleSolver
    {
        T? Puzzle { get; set; }
        public void Initialize(string puzzleInput)
        {
            Puzzle!.Initialize(puzzleInput);
        }
        public IEnumerable<string> SolveFirstPart()
        {
            yield return Puzzle?.SolveFirstPart() ?? string.Empty;
        }
        public IEnumerable<string> SolveSecondPart()
        {
            yield return Puzzle?.SolveSecondPart() ?? string.Empty;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PuzzleAttribute : Attribute
    {
        public int Number { get; private set; }
        public string Title { get; private set; }
        public PuzzleAttribute(int number, string title) => (Number, Title) = (number, title);
    }

    public class PuzzleHelper
    {
        public readonly IReadOnlyDictionary<int, (Type Type, int Number, string Title)> Puzzles = Assembly.GetExecutingAssembly().GetTypes()
        .Where(x => x.IsClass && !x.IsGenericType && (typeof(IPuzzleSolver).IsAssignableFrom(x) || typeof(IIncrementalPuzzleSolver).IsAssignableFrom(x)))
        .Select(x => (Type: x, Attr: x.GetCustomAttribute<PuzzleAttribute>()!))
        .Select(x => (x.Type, x.Attr.Number, x.Attr.Title))
        .ToDictionary(x => x.Number);
    }

    public record struct Voxel(int X, int Y, int Z);

    public record struct RangeOfCoordinates(Voxel LowerCoordinates, Voxel HigherCoordinates);

    public static class PointExtensions
    {
        public static Voxel Plus(this Voxel a, Voxel b) 
            => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static bool IsOutOfRange(this RangeOfCoordinates range, Voxel p)
            => p.X > range.HigherCoordinates.X || p.X < range.LowerCoordinates.X
            || p.Y > range.HigherCoordinates.Y || p.Y < range.LowerCoordinates.Y
            || p.Z > range.HigherCoordinates.Z || p.Z < range.LowerCoordinates.Z;
    }

    public static class DoubleExtensions
    {
        public static string ToStringCSS(this double value)
        {
            return value.ToString(CultureInfo.GetCultureInfo("en-GB"));
        }
    }

}
