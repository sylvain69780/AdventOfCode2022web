using sylvain69780.AdventOfCode2022.Domain;
using System.Reflection;
namespace sylvain69780.AdventOfCode2022.Blazor
{

    public static class PuzzleSolutionFactory
    {
        private static PuzzleSolutionAttribute? GetAttribute(Type puzzle) => (PuzzleSolutionAttribute?)puzzle.GetCustomAttribute(typeof(PuzzleSolutionAttribute));

        private static readonly List<(Type Type, PuzzleSolutionAttribute? Attribute)> _puzzleSolutions = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => typeof(IPuzzleSolver).IsAssignableFrom(x) && x.IsClass)
            .Select(x => (x, GetAttribute(x))).ToList();

        public static IPuzzleSolver? GetSolverByNumber(int number)
        {
            var type = _puzzleSolutions.Find(x => x.Attribute?.Number == number).Type;
            return (IPuzzleSolver?)Activator.CreateInstance(type);
        }
    }
}