using AdventOfCode2022Solutions.PuzzleSolutions;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Timers;

namespace AdventOfCode2022web.Shared
{
    public partial class PuzzleSolutionControllerNEW
    {
        [Inject]
        public HttpClient? Http { get; set; }
        [Parameter]
        public IPuzzleContext? PuzzleContext { get; set; }

        protected VisualizationSettings _settings = new() { AnimationDuration = 500 };

        public int SolvingStep { get; private set; } = 0;
        public PageState PageState { get; set; } = PageState.Loaded;
        private string _input = string.Empty;
        private IEnumerator<ProgressInfo>? _stepsToSolution;

        public string SampleInputFile()
        {
            var puzzleType = PuzzleContext!.GetType();
            var puzzleInputFile = puzzleType.Name.Replace("Context", "");
            return puzzleInputFile;
        }

        public string FullInputFile() => SampleInputFile() + "_full";

        public string PuzzleSolutionCode => $"https://github.com/sylvain69780/AdventOfCode2022web/blob/master/AdventOfCode2022/PuzzleSolutions/{PuzzleContext!.GetType().Name.Replace("Context", "")}";

        public async Task LoadDefaultPuzzleInput() => await LoadPuzzleInput(SampleInputFile());
        public async Task LoadFullPuzzleInput() => await LoadPuzzleInput(FullInputFile());

        public async Task LoadPuzzleInput(string puzzleInputFile)
        {
            _input = (await Http!.GetStringAsync($"sample-data/{puzzleInputFile}.txt")).Replace("\r", "");
            _stepsToSolution = PuzzleContext!.GetStepsToSolution(_input).GetEnumerator();
            MoveUntilCompleted();
        }

        protected override async Task OnInitializedAsync()
        {
            _stepComputationTimer.Elapsed += (sender, e) => MoveUntilCompleted();
            await LoadDefaultPuzzleInput();
        }

        public void MoveNext()
        {
            if (_stepsToSolution!.MoveNext())
                SolvingStep = _stepsToSolution!.Current.Step;
            else
                PageState = PageState.Finished;
        }

        public ProgressInfo? Result { get; private set; }


        public void StartPart1()
        {
            PageState = PageState.Processing;
        }

        public void StartPart2()
        {
        }

        private readonly System.Timers.Timer _stepComputationTimer = new();

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
            _stepComputationTimer.Stop();
            PageState = PageState.Loaded;
        }
    }
}
