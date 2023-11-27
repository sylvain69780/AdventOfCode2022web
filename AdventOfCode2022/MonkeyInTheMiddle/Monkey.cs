using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MonkeyInTheMiddle
{
    public class Monkey
    {
        public int Id;
        public List<long> WorryLevelOfItems = new();
        public char OperationToPerform;
        public int? ValueToAddOrMultiply;
        public long DivisibilityToTest;
        public int MonkeyRecipientIfDivisible;
        public int MonkeyRecipientIfNotDivisible;
        public long Inspections;
    }
}
