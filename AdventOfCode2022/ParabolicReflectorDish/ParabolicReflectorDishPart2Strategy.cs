using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ParabolicReflectorDish
{
    public class ParabolicReflectorDishPart2Strategy : IPuzzleStrategy<ParabolicReflectorDishModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(ParabolicReflectorDishModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var tortoiseData = (char[,])model.Platform!.Clone();
            var hareData = (char[,])tortoiseData.Clone();
            var width = tortoiseData.GetLength(0);
            var height = tortoiseData.GetLength(1);
            // https://en.wikipedia.org/wiki/Cycle_detection

            SpinCycle(tortoiseData, width, height);
            var tortoise = RockLoading(tortoiseData, width, height);
            SpinCycle(hareData, width, height);
            SpinCycle(hareData, width, height);
            var hare = RockLoading(hareData, width, height);

            while (tortoise != hare)
            {
                SpinCycle(tortoiseData, width, height);
                tortoise = RockLoading(tortoiseData, width, height);
                SpinCycle(hareData, width, height);
                SpinCycle(hareData, width, height);
                hare = RockLoading(hareData, width, height);
            }

            var mu = 0;
            tortoiseData = (char[,])model.Platform!.Clone();
            tortoise = RockLoading(tortoiseData, width, height);
            while (tortoise != hare)
            {
                SpinCycle(tortoiseData, width, height);
                tortoise = RockLoading(tortoiseData, width, height);
                SpinCycle(hareData, width, height);
                hare = RockLoading(hareData, width, height);
                mu += 1;
            }
            var lam = 1;
            hareData = (char[,])tortoiseData.Clone();
            SpinCycle(hareData, width, height);
            hare = RockLoading(hareData, width, height);
            while (tortoise != hare)
            {
                SpinCycle(hareData, width, height);
                hare = RockLoading(hareData, width, height);
                lam += 1;
            }

            var count = mu+(1000000000-mu) % lam;
            tortoiseData = (char[,])model.Platform!.Clone();
            for (var i=0;i<count;i++)
            {
                SpinCycle(tortoiseData, width, height);
                tortoise = RockLoading(tortoiseData, width, height);
            }

            yield return updateContext();
            provideSolution(tortoise.ToString());
        }

        private static int RockLoading(char[,] platform, int width, int height)
        {
            var sum = 0;
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    if (platform[x, y] == 'O')
                        sum += height - y;
                }

            return sum;
        }

        private static void SpinCycle(char[,] platform, int width, int height)
        {
            TiltNorth(platform, width, height);
            TiltWest(platform, width, height);
            TiltSouth(platform, width, height);
            TiltEast(platform, width, height);
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

        private static void TiltSouth(char[,] platform, int width, int height)
        {
            var moving = true;
            while (moving)
            {
                moving = false;
                for (var y = 0; y < height-1; y++)
                    for (var x = 0; x < width; x++)
                    {
                        if (platform[x, y] == 'O')
                            if (platform[x, y + 1] == '.')
                            {
                                platform[x, y + 1] = 'O';
                                platform[x, y] = '.';
                                moving = true;
                            }
                    }
            }
        }

        private static void TiltEast(char[,] platform, int width, int height)
        {
            var moving = true;
            while (moving)
            {
                moving = false;
                for (var y = 0; y < height ; y++)
                    for (var x = 0; x < width-1; x++)
                    {
                        if (platform[x, y] == 'O')
                            if (platform[x+1, y] == '.')
                            {
                                platform[x+1, y] = 'O';
                                platform[x, y] = '.';
                                moving = true;
                            }
                    }
            }
        }

        private static void TiltWest(char[,] platform, int width, int height)
        {
            var moving = true;
            while (moving)
            {
                moving = false;
                for (var y = 0; y < height; y++)
                    for (var x = 1; x < width; x++)
                    {
                        if (platform[x, y] == 'O')
                            if (platform[x - 1, y] == '.')
                            {
                                platform[x - 1, y] = 'O';
                                platform[x, y] = '.';
                                moving = true;
                            }
                    }
            }
        }

    }
}
