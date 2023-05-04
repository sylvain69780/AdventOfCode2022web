namespace AdventOfCode2022web.Domain.Puzzle
{
    [Puzzle(20, "Grove Positioning System")]
    public class GrovePositioningSystem : IPuzzleSolver
    {
        public IEnumerable<string> SolveFirstPart(string inp)
        {
            var c = 0;
            var input = inp.Split("\n").Select(x => (Idx: c++, Value: int.Parse(x))).ToList();
            foreach (var idx in Enumerable.Range(0, input.Count))
            {
                var elementIndex = input.FindIndex(x => x.Idx == idx);
                var element = input[elementIndex];
                Console.WriteLine($"Element {element.Value} at {elementIndex} moves now : " + string.Join(',', input.Select(x => x.Value)));
                if (element.Value == 0) continue;
                input.RemoveAt(elementIndex);
                if (element.Value > 0)
                {
                    var pos = (elementIndex + element.Value) % input.Count;
                    input.Insert(pos, element);
                }
                else
                {
                    var pos = (elementIndex + element.Value) % input.Count;
                    if (pos < 0) pos += input.Count;
                    if (pos == 0) input.Add(element);
                    else
                        input.Insert(pos, element);
                }
            }
            Console.WriteLine("END " + string.Join(',', input.Select(x => x.Value)));
            var selector = new int[3] { 1000, 2000, 3000 };
            var score = 0;
            foreach (var i in selector)
            {
                var zero = input.FindIndex(x => x.Value == 0);
                var index = (i + zero) % (input.Count);
                score += input[index].Value;
            }
            yield return $"SCORE {score}";
        }

        public IEnumerable<string> SolveSecondPart(string inp)
        {
            var c = 0;
            var input = inp.Split("\n").Select(x => (Idx: c++, Value: 811589153 * long.Parse(x))).ToList();
            for (var turns = 0; turns < 10; turns++)
            {
                foreach (var idx in Enumerable.Range(0, input.Count))
                {
                    var elementIndex = input.FindIndex(x => x.Idx == idx);
                    var element = input[elementIndex];
                    //Console.WriteLine($"Element {element.Value} at {elementIndex} moves now : " + string.Join(',', input.Select(x => x.Value)));
                    if (element.Value == 0) continue;
                    input.RemoveAt(elementIndex);
                    if (element.Value > 0)
                    {
                        var pos = (int)((elementIndex + element.Value) % input.Count);
                        input.Insert(pos, element);
                    }
                    else
                    {
                        var pos = (int)((elementIndex + element.Value) % input.Count);
                        if (pos < 0) pos += input.Count;
                        if (pos == 0) input.Add(element);
                        else
                            input.Insert(pos, element);
                    }
                }
            }
            Console.WriteLine("END " + string.Join(',', input.Select(x => x.Value)));
            var selector = new int[3] { 1000, 2000, 3000 };
            var score = 0L;
            foreach (var i in selector)
            {
                var zero = input.FindIndex(x => x.Value == 0);
                var index = (int)((i + zero) % (input.Count));
                score += input[index].Value;
            }
            yield return $"SCORE {score}";
        }
    }
}