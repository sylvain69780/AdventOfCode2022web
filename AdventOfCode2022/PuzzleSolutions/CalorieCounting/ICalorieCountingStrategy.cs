using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022Solutions.PuzzleSolutions.CalorieCounting
{
    public interface ICalorieCountingStrategy
    {
        public IEnumerable<ProgressInfo> GetStepsToSolution(IEnumerable<int> caloriesHoldByElves, Func<int, ProgressInfo> progressInfo, Action<string> provideSolution);
    }
}
