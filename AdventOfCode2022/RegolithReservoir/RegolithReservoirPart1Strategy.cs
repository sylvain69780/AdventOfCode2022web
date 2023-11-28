using Domain.BlizzardBasin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RegolithReservoir
{
    public class RegolithReservoirPart1Strategy : IPuzzleStrategy<RegolithReservoirModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(RegolithReservoirModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            model.OccupiedPositions.Clear();
            model.InitialPositions.Clear();
            var paths = model.PuzzleInput.Split("\n").Select(x => x.Replace(" -> ", "#").Split('#')
                .Select(y => y.Split(','))
                .Select(y => (x: int.Parse(y[0]), y: int.Parse(y[1]))).ToList())
                .ToList();
            var floorPosition = paths.SelectMany(x => x).Select(x => x.y).Max() + 2;
            foreach (var rocks in paths)
            {
                for (var i = 0; i < rocks.Count - 1; i++)
                {
                    var beginRock = rocks[i];
                    var endRock = rocks[i + 1];
                    if (beginRock.y == endRock.y)
                        for (var x = Math.Min(beginRock.x, endRock.x); x <= Math.Max(beginRock.x, endRock.x); x++)
                            model.SetOccupiedInitial((x, beginRock.y));
                    if (beginRock.x == endRock.x)
                        for (var y = Math.Min(beginRock.y, endRock.y); y <= Math.Max(beginRock.y, endRock.y); y++)
                            model.SetOccupiedInitial((beginRock.x, y));
                }
            }

            var iterations = 0;
            while (true)
            {
                model.SandPosition = (500, 0);
                var isFreeToMove = true;
                while (isFreeToMove && model.SandPosition.y < floorPosition)
                {
                    var newSandPosition = model.SandPosition;
                    foreach (var (dx, dy) in RegolithReservoirModel.Directions)
                    {
                        var (x, y) = (model.SandPosition.x + dx, model.SandPosition.y + dy);
                        if (!model.OccupiedPositions!.Contains((x, y)))
                        {
                            newSandPosition = (x, y);
                            break;
                        }
                    }
                    if (newSandPosition == model.SandPosition)
                        isFreeToMove = false;
                    else
                        model.SandPosition = newSandPosition;
                    yield return updateContext();
                }
                if (model.SandPosition.y >= floorPosition)
                    break;
                model.SetOccupied(model.SandPosition);
                iterations++;
            }
            yield return updateContext();
            provideSolution(iterations.ToString());
        }
    }
}
