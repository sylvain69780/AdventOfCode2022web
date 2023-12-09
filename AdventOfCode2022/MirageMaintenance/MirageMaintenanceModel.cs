using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MirageMaintenance
{
    public class MirageMaintenanceModel : IPuzzleModel
    {
        List<long>[]? _historicalData;
        public List<long>[] HistoricalData => _historicalData!;
        public void Parse(string input)
        {
            var i = input.Replace("\r","").Split("\n");
            _historicalData = i.Select(x => x.Split(" ").Select(x => long.Parse(x)).ToList()).ToArray();
        }

        public static void AddNextValue(List<long>[] historicalData)
        {
            for (var i = 0; i < historicalData.Length; i++)
            {
                var derivatives = new Stack<List<long>>();
                var h = historicalData[i];
                while (!h.All(x => x == 0))
                {
                    derivatives.Push(h);
                    var d = new List<long>();
                    for (var j = 1; j < h.Count; j++)
                    {
                        d.Add(h[j] - h[j - 1]);
                    }
                    h = d;
                }
                var derivValue = 0L;
                while (derivatives.TryPop(out var d))
                {
                    var addedValue = d[^1] + derivValue;
                    d.Add(addedValue);
                    derivValue = addedValue;
                }

            }
        }

    }
}
