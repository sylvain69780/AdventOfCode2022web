namespace AdventOfCode2022Solutions.PuzzleSolutions
{
    public class PuzzleOutputProvider
    {
        int _step = 0;
        public PuzzleOutput Put(string output)
        {
            return new PuzzleOutput()
            {
                Step = _step++,
                Output = output
            };
        }
    }
}
