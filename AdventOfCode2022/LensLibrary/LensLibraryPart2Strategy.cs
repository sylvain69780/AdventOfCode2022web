using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.LensLibrary
{
    public class LensLibraryPart2Strategy : IPuzzleStrategy<LensLibraryModel>
    {
        public string Name { get; set; } = "Part 2";

        public IEnumerable<ProcessingProgressModel> GetSteps(LensLibraryModel model, Func<ProcessingProgressModel> updateContext, Action<string> provideSolution)
        {
            var initSeq = model.InitializationSequence!;
            var sequence = initSeq
                .Select(x => Regex.Match(x, @"([a-z]+)([=|-])(\d*)"))
                .Select(x => (label: x.Groups[1].Value, operation: x.Groups[2].Value, focal: x.Groups[3].Value == string.Empty ? 0 : int.Parse(x.Groups[3].Value)))
                .ToList();
            var lensBoxes = new List<(string label, int focal)>[256];
            for (int i = 0; i < lensBoxes.Length; i++)
                lensBoxes[i] = new ();

            foreach (var (label, operation, focal) in sequence)
            {
                var hash = LensLibraryHelpers.Hash(label);
                var box = lensBoxes[hash];
                if ( operation == "-")
                {
                    var lens = box.Select((x, i) => (Item:x, Index:i)).Where(y => y.Item.label == label).ToList();
                    if (lens.Count > 0)
                        box.RemoveAt(lens[0].Index);
                }
                if ( operation == "=")
                {
                    var lens = box.Select((x, i) => (Item: x, Index: i)).Where(y => y.Item.label == label).ToList();
                    if (lens.Count > 0)
                        box[lens[0].Index] = (label, focal);
                    else
                        box.Add((label, focal));
                }
            }
            var focusingPower = lensBoxes.Select((box, boxIndex) => box.Select((lens, lensSlot) => (1 + boxIndex) * (1 + lensSlot) * lens.focal).Sum()).Sum();
            yield return updateContext();
            provideSolution(focusingPower.ToString());
        }
    }
}
