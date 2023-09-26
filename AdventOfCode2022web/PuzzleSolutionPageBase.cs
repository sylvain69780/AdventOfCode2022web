using AdventOfCode2022Solutions.PuzzleSolutions;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Timers;

namespace AdventOfCode2022web
{
    public class PuzzleSolutionPageBase<T> : ComponentBase where T : IPuzzleSolutionIter, new()
    {
        [Inject]
        public HttpClient? Http { get; set; }

        public int AnimationDuration { get; set; } = 500;
        public int SolvingStep { get; private set; } = 0;
        public bool Finished = true;
        public T PuzzleSolution { get; } = new T();
        public string Input { get; set; } = string.Empty;

        public string SampleInputFile()
        {
            var puzzleType = PuzzleSolution!.GetType();
            var puzzleInputFile = puzzleType.Name.Replace("Solution", "");
            return puzzleInputFile;
        }

        public string FullInputFile() => SampleInputFile() + "_full";
       
        public string PuzzleSolutionCode => $"https://github.com/sylvain69780/AdventOfCode2022web/blob/master/AdventOfCode2022/PuzzleSolutions/{PuzzleSolution.GetType().Name.Replace("Solution","")}";

        public async Task LoadDefaultPuzzleInput() => await LoadPuzzleInput(SampleInputFile());
        public async Task LoadFullPuzzleInput() => await LoadPuzzleInput(FullInputFile());

        public async Task LoadPuzzleInput(string puzzleInputFile)
        {
            Input = (await Http!.GetStringAsync($"sample-data/{puzzleInputFile}.txt")).Replace("\r", "");
            Stop();
            PuzzleSolution.Initialize(Input);
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDefaultPuzzleInput();
            _stepComputationTimer.Elapsed += (object? sender, ElapsedEventArgs e) => MoveUntilCompleted();
        }

        private IEnumerator<string>? _results;
        public string Result { get; private set; } = string.Empty;


        public void StartPart1()
        {
            _results = PuzzleSolution!.SolveFirstPart().GetEnumerator();
            Finished = false;
            SolvingStep = 0;
        }

        public void StartPart2()
        {
            _results = PuzzleSolution!.SolveSecondPart().GetEnumerator();
            Finished = false;
            SolvingStep = 0;
        }

        public void MoveNext()
        {
            if (_results!.MoveNext())
            {
                Result = _results.Current;
                SolvingStep++;
            }
            else
                Finished = true;
        }

        private System.Timers.Timer _stepComputationTimer = new ();

        public void MoveUntilCompleted()
        {
            _stepComputationTimer.Stop();
            MoveNext();
            if ( AnimationDuration == 0)
            {
                var stopWatch = Stopwatch.StartNew();
                while (!Finished && stopWatch.ElapsedMilliseconds < 2000)
                    MoveNext();
            }
            if (!Finished)
            {
                _stepComputationTimer!.Interval = AnimationDuration == 0 ? 100 : AnimationDuration;
                _stepComputationTimer.Start();
            }
            StateHasChanged();
        }

        public virtual void Dispose()
        {
            _stepComputationTimer!.Stop();
            _stepComputationTimer.Dispose();
        }

        public void Stop()
        {
            _results = null;
            _stepComputationTimer.Stop();
            Finished = true;
        }
    }
}
