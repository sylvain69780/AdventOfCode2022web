using System.Text.RegularExpressions;

namespace AdventOfCode2022web.Puzzles
{
    [Puzzle(21, "Monkey Math")]
    public class MonkeyMath : IPuzzleSolver
    {
        public IEnumerable<string> SolveFirstPart(string inp)
        {
            var input = inp.Split("\n");
            var r1 = new Regex(@"([a-z]+): ([a-z]+) ([\+\-\/\*]) ([a-z]+)");
            var r2 = new Regex(@"([a-z]+): (\d+)");
            var nodes = input.Select(x => r1.Match(x))
                .Where(x => x.Success)
                .ToDictionary(x => x.Groups[1].Value, x => (Left: x.Groups[2].Value, Operator: x.Groups[3].Value, Right: x.Groups[4].Value));
            var values = input.Select(x => r2.Match(x))
                .Where(x => x.Success)
                .ToDictionary(x => x.Groups[1].Value, x => long.Parse(x.Groups[2].Value));
            var search = new Stack<string>();
            search.Push("root");
            while (search.TryPop(out var element))
            {
                if (values.ContainsKey(element))
                    continue;
                else
                    search.Push(element);
                var (Left, Operator, Right) = nodes[element];
                if (values.TryGetValue(Left, out var left))
                    if (values.TryGetValue(Right, out var right))
                    {
                        var result =
                            Operator == "+" ? left + right :
                            Operator == "-" ? left - right :
                            Operator == "*" ? left * right :
                            Operator == "/" ? left / right : throw (new NotImplementedException());
                        values.Add(element, result);
                    }
                    else
                        search.Push(Right);
                else
                    search.Push(Left);
            }
            yield return $"Score : {values["root"]}";
        }
        public IEnumerable<string> SolveSecondPart(string inp)
        {
            var input = inp.Split("\n");
            var r1 = new Regex(@"([a-z]+): ([a-z]+) ([\+\-\/\*]) ([a-z]+)");
            var r2 = new Regex(@"([a-z]+): (\d+)");
            var nodes = input.Select(x => r1.Match(x))
                .Where(x => x.Success)
                .ToDictionary(x => x.Groups[1].Value, x => (Left: x.Groups[2].Value, Operator: x.Groups[3].Value, Right: x.Groups[4].Value));
            var values = input.Select(x => r2.Match(x))
                .Where(x => x.Success)
                .Select(x => (Key: x.Groups[1].Value, Value: long.Parse(x.Groups[2].Value)))
                .ToList();
            var compute = (long guess) =>
            {
                var valuesFound = values.ToDictionary(x => x.Key, x => x.Value);
                valuesFound["humn"] = guess;
                var search = new Stack<string>();
                search.Push("root");
                while (search.TryPop(out var element))
                {
                    if (valuesFound.ContainsKey(element))
                        continue;
                    else
                        search.Push(element);
                    var (Left, Operator, Right) = nodes[element];
                    if (valuesFound.TryGetValue(Left, out var left))
                        if (valuesFound.TryGetValue(Right, out var right))
                        {
                            var result =
                                Operator == "+" ? left + right :
                                Operator == "-" ? left - right :
                                Operator == "*" ? left * right :
                                Operator == "/" ? left / right : throw (new NotImplementedException());
                            valuesFound.Add(element, result);
                        }
                        else
                            search.Push(Right);
                    else
                        search.Push(Left);
                }
                var root = nodes["root"];
                return valuesFound[root.Left] - valuesFound[root.Right];
            };

            var guessMin = 0L;
            var guessMax = long.MaxValue / 1000000;
            var guess = guessMax / 2;
            var result = 1L;
            do
            {
                result = compute(guess);
                if (result < 0)
                    guessMax = guess;
                if (result > 0)
                    guessMin = guess;
                Console.WriteLine($"For {guess} => {result}");
                yield return $"For {guess} => {result}";
                guess = guessMin + (guessMax - guessMin) / 2;
            } while (result != 0 || guessMin == guess);
            yield return $"value = {guess}";
        }
    }
}