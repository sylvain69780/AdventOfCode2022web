namespace Domain.HotSprings
{
    internal static class HotSpringsHelper
    {
        public static IEnumerable<string> PossibleConfigurations(this string jockerString)
        {
            if (jockerString == string.Empty)
                yield return string.Empty;
            else
            if (jockerString[0] == '?')
                foreach (var c in "#.")
                    foreach (var sub in jockerString[1..].PossibleConfigurations())
                        yield return c + sub;
            else
                foreach (var sub in jockerString[1..].PossibleConfigurations())
                    yield return jockerString[0] + sub;
        }
        public static int PossibleConfigurationsOptim(this string jockerString, long[] ranges)
        {
            var sumOfBroken = ranges.Sum();
            var sumOfGood = jockerString.Length - sumOfBroken;
            var stack = new Stack<string>();
            stack.Push("");
            var counter = 0;
            while (stack.Count > 0)
            {
                var newStack = new Stack<string>();
                while (stack.TryPop(out var s))
                {
                    if (s.Length == jockerString.Length)
                    {
                        counter++;
                        continue;
                    }
                    var currentRanges = s.Groups().ToArray();
                    var c = jockerString[s.Length];
                    if (c == '?' || c == '#')
                        if (currentRanges.Sum() < sumOfBroken)
                        {
                            if ((currentRanges.Length == 0 || s[^1] == '.') && currentRanges.Length < ranges.Length)
                                newStack.Push(s + '#');
                            else if (currentRanges[^1] < ranges[currentRanges.Length - 1])
                                newStack.Push(s + '#');
                        }
                    if (c == '?' || c == '.')
                    {
                        if (s.Where(x => x == '.').Count() < sumOfGood)
                            if (currentRanges.Length == 0 || currentRanges[^1] == ranges[currentRanges.Length - 1])
                                newStack.Push(s + '.');
                    }
                }
                stack = newStack;
            }
            return counter;
        }

        public static IEnumerable<string> PossibleConfigurationsOptimBAD(this string jockerString, long[] ranges)
        {
            var cache = new Dictionary<string, long[]>();
            var fromCache = (string s) =>
            {
                if (cache.TryGetValue(s, out var cachedResult))
                {
                    return cachedResult;
                }
                else
                {
                    var result = s.Groups().ToArray();
                    cache[s] = result;
                    return result;
                }
            };
            var sumOfBroken = ranges.Sum();
            var sumOfGood = jockerString.Length - sumOfBroken;
            var stack = new Stack<string>();
            stack.Push("");
            while (stack.Count > 0)
            {
                var newStack = new Stack<string>();
                while (stack.TryPop(out var s))
                {
                    if (s.Length == jockerString.Length)
                    {
                        yield return s;
                        continue;
                    }
                    var currentRanges = fromCache(s);
                    var c = jockerString[s.Length];
                    if (c == '?' || c == '#')
                        if (currentRanges.Sum() < sumOfBroken)
                        {
                            if ((currentRanges.Length == 0 || s[^1] == '.') && currentRanges.Length < ranges.Length)
                                newStack.Push(s + '#');
                            else if (currentRanges[^1] < ranges[currentRanges.Length - 1])
                                newStack.Push(s + '#');
                        }
                    if (c == '?' || c == '.')
                    {
                        if (s.Where(x => x == '.').Count() < sumOfGood)
                            if (currentRanges.Length == 0 || currentRanges[^1] == ranges[currentRanges.Length - 1])
                                newStack.Push(s + '.');
                    }
                }
                stack = newStack;
            }
        }

        public static IEnumerable<long> Groups(this string s)
        {
            if (s == string.Empty)
                yield break;
            var count = 1L;
            var c = s[0];
            for (var i = 1; i < s.Length; i++)
            {
                if (s[i] != c)
                {
                    if (c == '#')
                        yield return count;
                    c = s[i];
                    count = 0;
                }
                count++;
            }
            if (c == '#')
                yield return count;
        }

        public static IEnumerable<long> GroupsAll(this string s)
        {
            var count = 1L;
            var c = s[0];
            for (var i = 1; i < s.Length; i++)
            {
                if (s[i] != c)
                {
                    yield return count;
                    c = s[i];
                    count = 0;
                }
                count++;
            }
            yield return count;
        }
    }
}
