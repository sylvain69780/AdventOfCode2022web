namespace AdventOfCode2022Solutions.PuzzleSolutions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PuzzleSolutionAttribute : Attribute
    {
        public int Number { get; }
        public PuzzleSolutionAttribute(int number)
        {
            Number = number;
        }
    }
}