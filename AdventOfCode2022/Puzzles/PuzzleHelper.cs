﻿using System.Reflection;

namespace AdventOfCode2022web.Puzzles
{
    public interface IPuzzleSolver
    {
        string SolveFirstPart(string input);
        string SolveSecondPart(string input);
    }

    public interface IPuzzleSolverV2
    {
        Task<string> SolveFirstPart(string input, Func<string, Task> update, CancellationToken cancellationToken);
        Task<string> SolveSecondPart(string input, Func<string, Task> update, CancellationToken cancellationToken);
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
        .Where(x => x.IsClass && (typeof(IPuzzleSolver).IsAssignableFrom(x) || typeof(IPuzzleSolverV2).IsAssignableFrom(x)))
        .Select(x => (Type: x, Attr: x.GetCustomAttribute<PuzzleAttribute>()!))
        .Select(x => (x.Type, x.Attr.Number, x.Attr.Title))
        .ToDictionary(x => x.Number);
    }

    public record struct Point(int X, int Y, int Z);
}
