using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CosmicExpansion
{
    public class CosmicExpansionPart2Strategy : IPuzzleStrategy<CosmicExpansionModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(CosmicExpansionModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var galaxies = model.Galaxies!.ToDictionary(a => a.id, a => (a.x, a.y));
            var xDistanceOfGalaxies = galaxies.GroupBy(a => a.Value.x, a => a.Key)
                .OrderBy(a => a.Key)
                .ToArray();
            var yDistanceOfGalaxies = galaxies.GroupBy(a => a.Value.y, a => a.Key)
                .OrderBy(a => a.Key)
                .ToArray();
            for (var i = 1; i < xDistanceOfGalaxies.Length; i++)
            {
                var distance = xDistanceOfGalaxies[i].Key - xDistanceOfGalaxies[i - 1].Key - 1;
                if (distance == 0)
                    continue;
                for (var j = i; j < xDistanceOfGalaxies.Length; j++)
                {
                    foreach (var id in xDistanceOfGalaxies[j])
                    {
                        var galaxy = galaxies[id];
                        galaxies[id] = (galaxy.x - distance + 1000000*distance, galaxy.y);
                    }
                }
            }
            for (var i = 1; i < yDistanceOfGalaxies.Length; i++)
            {
                var distance = yDistanceOfGalaxies[i].Key - yDistanceOfGalaxies[i - 1].Key - 1;
                if (distance == 0)
                    continue;
                for (var j = i; j < yDistanceOfGalaxies.Length; j++)
                {
                    foreach (var id in yDistanceOfGalaxies[j])
                    {
                        var galaxy = galaxies[id];
                        galaxies[id] = (galaxy.x, galaxy.y - distance + 1000000 * distance);
                    }
                }
            }

            var pairs = galaxies.Keys.SelectMany(g1 => galaxies.Keys.Select(g2 => (g1, g2)).Where(p => p.g1 < p.g2)).ToList();

            var sum = pairs.Select(p => Math.Abs(galaxies[p.g2].x - galaxies[p.g1].x) + Math.Abs(galaxies[p.g2].y - galaxies[p.g1].y)).Sum();
            yield return updateContext();
            provideSolution(sum.ToString());
        }
    }
}
