using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ParabolicReflectorDish
{
    public class ParabolicReflectorDishPart1Strategy : IPuzzleStrategy<ParabolicReflectorDishModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(ParabolicReflectorDishModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var platform = model.Platform!;
            var width = platform.GetLength(0);
            var height = platform.GetLength(1);
            // tilt
            TiltNorth(platform, width, height);
            var sum = 0;
            sum = RockLoading(platform, width, height, sum);

            yield return updateContext();
            provideSolution(sum.ToString());
        }

        private static int RockLoading(char[,] platform, int width, int height, int sum)
        {
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    if (platform[x, y] == 'O')
                        sum += height - y;
                }

            return sum;
        }

        private static void TiltNorth(char[,] platform, int width, int height)
        {
            var moving = true;
            while (moving)
            {
                moving = false;
                for (var y = 1; y < height; y++)
                    for (var x = 0; x < width; x++)
                    {
                        if (platform[x, y] == 'O')
                            if (platform[x, y - 1] == '.')
                            {
                                platform[x, y - 1] = 'O';
                                platform[x, y] = '.';
                                moving = true;
                            }
                    }
            }
        }
    }
}
