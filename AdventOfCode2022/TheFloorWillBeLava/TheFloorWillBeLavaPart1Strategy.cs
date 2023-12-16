﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TheFloorWillBeLava
{
    public class TheFloorWillBeLavaPart1Strategy : IPuzzleStrategy<TheFloorWillBeLavaModel>
    {
        public string Name { get; set; } = "Part 1";

        public IEnumerable<ProcessingProgressModel> GetSteps(TheFloorWillBeLavaModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var map = model.Layout!;
            var maxDistance = Math.Max(map.Length, map[0].Length);
            var energyMap = new int[map[0].Length, map.Length];
            var start = (x: 0, y: 0, dir: '>');
            var queue = new Queue<(int x, int y, char dir)>();
            queue.Enqueue(start);
            var lastEnergized = 0;
            var filter = new HashSet<(int x, int y, char dir)>();
            while (queue.Count > 0 && lastEnergized < maxDistance)
            {
                lastEnergized++;
                var newQueue = new Queue<(int x, int y, char dir)>();
                filter.Clear();
                while (queue.TryDequeue(out var beam))
                {
                    if (filter.Contains(beam))
                        continue;
                    else
                        filter.Add(beam);
                    var c = map.GetTile(beam.x, beam.y);
                    if (c == '#')
                        continue;
                    else
                    {
                        if (energyMap[beam.x, beam.y] == 0)
                        {
                            energyMap[beam.x, beam.y] = 1;
                            lastEnergized = 0;
                        }
                    }
                    if (c == '.')
                    {
                        if (beam.dir == '>')
                            newQueue.Enqueue((beam.x + 1, beam.y, '>'));
                        else if (beam.dir == '<')
                            newQueue.Enqueue((beam.x - 1, beam.y, '<'));
                        else if (beam.dir == 'v')
                            newQueue.Enqueue((beam.x, beam.y + 1, 'v'));
                        else if (beam.dir == '^')
                            newQueue.Enqueue((beam.x, beam.y - 1, '^'));
                        continue;
                    }
                    if (c == '-')
                    {
                        if (beam.dir == '>')
                            newQueue.Enqueue((beam.x + 1, beam.y, '>'));
                        if (beam.dir == '<')
                            newQueue.Enqueue((beam.x - 1, beam.y, '<'));
                        if (beam.dir == 'v' || beam.dir == '^')
                        {
                            newQueue.Enqueue((beam.x + 1, beam.y, '>'));
                            newQueue.Enqueue((beam.x - 1, beam.y, '<'));
                        }
                        continue;
                    }
                    if (c == '|')
                    {
                        if (beam.dir == 'v')
                            newQueue.Enqueue((beam.x, beam.y + 1, 'v'));
                        if (beam.dir == '^')
                            newQueue.Enqueue((beam.x, beam.y - 1, '^'));
                        if (beam.dir == '>' || beam.dir == '<')
                        {
                            newQueue.Enqueue((beam.x, beam.y + 1, 'v'));
                            newQueue.Enqueue((beam.x, beam.y - 1, '^'));
                        }
                        continue;
                    }
                    if (c == '/')
                    {
                        if (beam.dir == '>')
                            newQueue.Enqueue((beam.x, beam.y - 1, '^'));
                        if (beam.dir == '<')
                            newQueue.Enqueue((beam.x, beam.y + 1, 'v'));
                        if (beam.dir == 'v')
                            newQueue.Enqueue((beam.x - 1, beam.y, '<'));
                        if (beam.dir == '^')
                            newQueue.Enqueue((beam.x + 1, beam.y, '>'));
                        continue;
                    }
                    if (c == '\\')
                    {
                        if (beam.dir == '>')
                            newQueue.Enqueue((beam.x, beam.y + 1, 'v'));
                        if (beam.dir == '<')
                            newQueue.Enqueue((beam.x, beam.y - 1, '^'));
                        if (beam.dir == 'v')
                            newQueue.Enqueue((beam.x + 1, beam.y, '>'));
                        if (beam.dir == '^')
                            newQueue.Enqueue((beam.x - 1, beam.y, '<'));
                        continue;
                    }
                }
                queue = newQueue;
            }
            var sum = 0;
            for (var y = 0; y < energyMap.GetLength(1); y++)
                for (var x = 0; x < energyMap.GetLength(0); x++)
                    sum += energyMap[x, y];
            yield return updateContext();
            provideSolution(sum.ToString());
        }
    }
}
