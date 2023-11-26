using Domain.BlizzardBasin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TreetopTreeHouse
{
    public class TreetopTreeHousePart1Strategy : IPuzzleStrategy<TreetopTreeHouseModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(TreetopTreeHouseModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var visibleTrees = 0;
            foreach (var yTree in Enumerable.Range(0, model.Height))
                foreach (var xTree in Enumerable.Range(0, model.Width))
                {
                    var treeHeight = model.TreeHeight(xTree, yTree);
                    foreach (var (dx, dy) in TreetopTreeHouseModel.Directions)
                    {
                        var distance = 0;
                        bool borderReached;
                        var (x, y) = (0, 0);
                        do
                        {
                            distance++;
                            (x, y) = (xTree + dx * distance, yTree + dy * distance);
                            borderReached = model.IsOutOfMap(x, y);
                        } while (!borderReached && model.TreeHeight(x, y) < treeHeight);
                        if (borderReached)
                        {
                            visibleTrees++;
                            break;
                        }
                    }
                }
            yield return updateContext();
            provideSolution(visibleTrees.ToString());
        }
    }
}
