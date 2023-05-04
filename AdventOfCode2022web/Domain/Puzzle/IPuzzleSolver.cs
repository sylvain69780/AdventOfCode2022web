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
}
