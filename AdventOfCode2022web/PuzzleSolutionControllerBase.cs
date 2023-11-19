using Domain;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Timers;
namespace Blazor
{

    public class PuzzleSolutionControllerBase : ComponentBase
    {
        [Inject]
        public HttpClient? Http { get; set; }
        [Parameter]
        public IPuzzleSolver? PuzzleSolver { get; set; }

        protected VisualizationSettings _settings = new() { AnimationDuration = 500 };

        public int SolvingStep { get; private set; } = 0;
        public PageState PageState { get; set; } = PageState.Loaded;
        public string Input { get; set; } = string.Empty;

        public string SampleInputFile()
        {
            var puzzleType = PuzzleSolver!.GetType();
            var puzzleInputFile = puzzleType.Name.Replace("Solution", "");
            return puzzleInputFile;
        }

        public string FullInputFile() => SampleInputFile() + "_full";

        public string PuzzleSolutionCode => $"https://github.com/sylvain69780/AdventOfCode2022web/blob/master/AdventOfCode2022/PuzzleSolutions/{PuzzleSolver.GetType().Name.Replace("Solution", "")}";

        public async Task LoadDefaultPuzzleInput() => await LoadPuzzleInput(SampleInputFile());
        public async Task LoadFullPuzzleInput() => await LoadPuzzleInput(FullInputFile());

        public async Task LoadPuzzleInput(string puzzleInputFile)
        {
            Input = (await Http!.GetStringAsync($"sample-data/{puzzleInputFile}.txt")).Replace("\r", "");
            Stop();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDefaultPuzzleInput();
            _stepComputationTimer.Elapsed += (object? sender, ElapsedEventArgs e) => MoveUntilCompleted();
            StartPart1();
            MoveUntilCompleted();
        }

        private IEnumerator<PuzzleOutput>? _results;
        public PuzzleOutput? Result { get; private set; }


        public void StartPart1()
        {
            _results = PuzzleSolver!.SolveFirstPart(Input).GetEnumerator();
            PageState = PageState.Processing;
            SolvingStep = 0;
        }

        public void StartPart2()
        {
            _results = PuzzleSolver!.SolveSecondPart(Input).GetEnumerator();
            PageState = PageState.Processing;
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
                PageState = PageState.Finished;
        }

        private System.Timers.Timer _stepComputationTimer = new();

        public void MoveUntilCompleted()
        {
            PageState = PageState.ProcessingAuto;
            _stepComputationTimer.Stop();
            MoveNext();
            if (_settings.AnimationDuration == 0)
            {
                var stopWatch = Stopwatch.StartNew();
                while (PageState != PageState.Finished && stopWatch.ElapsedMilliseconds < 2000)
                    MoveNext();
            }
            if (PageState != PageState.Finished)
            {
                _stepComputationTimer!.Interval = _settings.AnimationDuration == 0 ? 100 : _settings.AnimationDuration;
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
            PageState = PageState.Loaded;
        }
    }
}