namespace Domain.PyroclasticFlow
{
    public class RockGenerator
    {
        private static readonly (int x, int y)[][] BlockShapes = new (int x, int y)[][]
            {
                new (int x, int y)[] {(0,0), (1,0), (2,0), (3,0)},          // horizontal bar
                new (int x, int y)[] {(1,0), (0,1), (1,1), (2,1), (1,2)},   // cross
                new (int x, int y)[] {(0,0), (1,0), (2,0), (2,1), (2,2)},   // L
                new (int x, int y)[] {(0,0), (0,1), (0,2), (0,3) },         // vertical bar
                new (int x, int y)[] {(0,0), (1,0), (0,1), (1,1) }          // square
            };

        public int Counter;

        public (int x, int y)[] FetchRockShape()
        {
            var counter = Counter;
            Counter = (counter + 1) % 5;
            return BlockShapes[counter];
        }
    }
}