namespace AdventOfCode2022web.Domain.Puzzle
{
    public class TuningTrouble : PuzzleSolver
    {
        private static string Format(int v) => v.ToString();

        protected override string Part1(string inp)
        {
            var marker = new Queue<char>();
            var pos = 1;
            foreach (var c in inp)
            {
                if (marker.Count == 4) marker.Dequeue();
                marker.Enqueue(c);
                if (marker.Count == 4 && marker.GroupBy(x => x).Select(y => y.Count()).Max() == 1) break;
                pos++;
            }
            return Format(pos);
        }
        protected override string Part2(string inp)
        {
            var marker = new Queue<char>();
            var pos = 1;
            foreach (var c in inp)
            {
                if (marker.Count == 14) marker.Dequeue();
                marker.Enqueue(c);
                if (marker.Count == 14 && marker.GroupBy(x => x).Select(y => y.Count()).Max() == 1) break;
                pos++;
            }
            return Format(pos);
        }
    }

}