namespace Domain
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
        public PuzzleOutput Put(string output,object info)
        {
            return new PuzzleOutput()
            {
                Step = _step++,
                Output = output,
                Info  = info,
            };
        }
    }
}
