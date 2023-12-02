using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CubeConundrum
{
    public class CubeConundrumPart1Strategy : IPuzzleStrategy<CubeConundrumModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(CubeConundrumModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var games = model.InformationForEachGame!;
            var guessedBag = new Dictionary<string,int>()
            {
                { "red", 12 },
                { "green",13 },
                { "blue",14 }
            };
            var idsOfPossibleGames = new List<int>();
            for (var i = 0; i<games.Count; i++)
            {
                var possibleGame = true;
                var game = games[i];
                foreach (var drawing in game)
                {
                    if (drawing.Any(x => x.count > guessedBag[x.color]))
                    {
                        possibleGame = false;
                        break;
                    }
                }
                if (possibleGame)
                    idsOfPossibleGames.Add(i + 1);
            }
            yield return updateContext();
            provideSolution(idsOfPossibleGames.Sum().ToString());
        }
    }
}
