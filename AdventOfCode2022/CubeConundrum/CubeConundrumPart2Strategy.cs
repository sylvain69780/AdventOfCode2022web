using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CubeConundrum
{
    public class CubeConundrumPart2Strategy : IPuzzleStrategy<CubeConundrumModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(CubeConundrumModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var games = model.InformationForEachGame!;
            var powerOfEachGame = new List<int>();
            foreach (var game in games)
            {
                var guessedBag = new Dictionary<string, int>()
                    {
                        { "red", 0 },
                        { "green",0 },
                        { "blue",0 }
                    };
                foreach (var drawing in game)
                    foreach (var (count, color) in drawing)
                        guessedBag[color] = Math.Max(guessedBag[color], count);
                powerOfEachGame.Add(guessedBag.Values.Aggregate(1, (x, y) => x * y));
            }
            yield return updateContext();
            provideSolution(powerOfEachGame.Sum().ToString());
        }
    }
}
