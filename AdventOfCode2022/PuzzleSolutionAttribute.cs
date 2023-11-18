namespace sylvain69780.AdventOfCode2022.Domain
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