using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MonkeyInTheMiddle
{
    public class MonkeyInTheMiddleModel : IPuzzleModel
    {
        public void Parse(string puzzleInput)
        {
            var input = puzzleInput.Split("\n").AsEnumerable().GetEnumerator();
            var monkeys = new List<Monkey>();
            var counter = 0;
            while (input.MoveNext())
            {
                input.MoveNext();
                var worryLevelOfItems = input.Current[18..].Split(',').Select(x => long.Parse(x)).ToList();
                input.MoveNext();
                var operAndValue = input.Current[19..].Split(' ');
                input.MoveNext();
                var divisibilityToTest = int.Parse(input.Current[21..]);
                input.MoveNext();
                var ifTrue = int.Parse(input.Current[29..]);
                input.MoveNext();
                var ifFalse = int.Parse(input.Current[29..]);
                input.MoveNext();
                monkeys.Add(new Monkey
                {
                    Id = counter++,
                    WorryLevelOfItems = worryLevelOfItems,
                    OperationToPerform = operAndValue[1][0],
                    ValueToAddOrMultiply = operAndValue[2] == "old" ? null : int.Parse(operAndValue[2]),
                    DivisibilityToTest = divisibilityToTest,
                    MonkeyRecipientIfDivisible = ifTrue,
                    MonkeyRecipientIfNotDivisible = ifFalse
                });
            }
            _monkeys= monkeys;
        }

        List<Monkey> _monkeys = new();
        public List<Monkey> Monkeys => _monkeys;
    }
}
