using Domain.CamelCards;

internal static class HandHelpers
{

    public static HandType GetHandType(this string hand)
    {
        var g = hand.GroupBy(x => x).Select(x => (Card: x.Key, Count: x.Count())).OrderByDescending(x => x.Count).ToArray();
        if (g[0].Count == 5)
            return HandType.FiveOfAkind;
        if (g[0].Count == 4)
            return HandType.FourOfAKind;
        if (g[0].Count == 3 && g[1].Count == 2)
            return HandType.FullHouse;
        if (g[0].Count == 3)
            return HandType.ThreeOfAKind;
        if (g[0].Count == 2 && g[1].Count == 2)
            return HandType.TwoPair;
        if (g[0].Count == 2)
            return HandType.OnePair;
        return HandType.HighCard;
    }

    public static IEnumerable<string> PossibleHands(this string handJocker)
    {
        if (handJocker.Length == 1)
            if (handJocker[0] == 'J')
                foreach (var c in "AKQT98765432")
                    yield return c + "";
            else
                yield return handJocker;
        else
        if (handJocker[0] == 'J')
            foreach (var c in "AKQT98765432")
                foreach (var sub in handJocker[1..].PossibleHands())
                    yield return c + sub;
        else
            foreach (var sub in handJocker[1..].PossibleHands())
                yield return handJocker[0] + sub;
    }
}