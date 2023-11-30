namespace Domain.MonkeyMap
{
    public class Simulation
    {
        public string[]? Map;
        public List<(int Move, string Rotation)>? Instructions;
        public (int X, int Y) Position;
        public Direction Direction;
        public int MaxY => Map!.Length - 1;
        public int MaxX => Map![Position.Y].Length - 1;
        public int Side;
        public int Step;
    }
}