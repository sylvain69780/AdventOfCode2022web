using Domain.BlizzardBasin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TreetopTreeHouse
{
    public class TreetopTreeHousePart2Strategy : IPuzzleStrategy<TreetopTreeHouseModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(TreetopTreeHouseModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var scoreMax = 0;
            foreach (var yTree in Enumerable.Range(0, model.Height))
                foreach (var xTree in Enumerable.Range(0, model.Width))
                {
                    var treeHeight = model.TreeHeight(xTree, yTree);
                    var score = 1;
                    foreach (var (dx, dy) in TreetopTreeHouseModel.Directions)
                    {
                        var distance = 1;
                        var (x, y) = (xTree + distance * dx, yTree + distance * dy);
                        while (!model.IsOutOfMap(x, y) && model.TreeHeight(x, y) < treeHeight)
                        {
                            distance++;
                            (x, y) = (xTree + distance * dx, yTree + distance * dy);
                        }
                        score *= distance - (model.IsOutOfMap(x, y) ? 1 : 0);
                    }
                    scoreMax = Math.Max(score, scoreMax);
                }
            yield return updateContext();
            provideSolution(scoreMax.ToString());
        }
    }
}
