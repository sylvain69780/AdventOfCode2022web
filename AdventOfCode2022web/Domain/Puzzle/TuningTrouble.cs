namespace AdventOfCode2022web.Domain.Puzzle
{
    public class TuningTrouble : IPuzzleSolver
    {
        public string Input { get; set; } = String.Empty;
        public string Part1()
        {
            var marker = new Queue<char>();
            var pos = 1;
            foreach (var c in Input)
            {
                if (marker.Count == 4) marker.Dequeue();
                marker.Enqueue(c);
                if (marker.Count == 4 && marker.GroupBy(x => x).Select(y => y.Count()).Max() == 1) break;
                pos++;
            }
            return pos.ToString();
        }
        public string Part2()
        {
            var marker = new Queue<char>();
            var pos = 1;
            foreach (var c in Input)
            {
                if (marker.Count == 14) marker.Dequeue();
                marker.Enqueue(c);
                if (marker.Count == 14 && marker.GroupBy(x => x).Select(y => y.Count()).Max() == 1) break;
                pos++;
            }
            return pos.ToString();
        }
    }

}