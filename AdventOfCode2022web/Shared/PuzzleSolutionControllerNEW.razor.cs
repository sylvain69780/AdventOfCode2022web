using Domain;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
namespace Blazor.Shared
{
    public partial class PuzzleSolutionControllerNEW
    {
        [Inject]
        public HttpClient? Http { get; set; }
        [Parameter]
        public IPuzzleService? PuzzleContext { get; set; }

        protected VisualizationSettings _settings = new() { AnimationDuration = 500 };

        public int SolvingStep { get; private set; } = 0;
        public PageState PageState { get; set; } = PageState.Loaded;
        private string _input = string.Empty;
        private IEnumerator<ProcessingProgressModel>? _stepsToSolution;

        public string SampleInputFile()
        {
            var puzzleType = PuzzleContext!.GetType();
            var puzzleInputFile = puzzleType.Name.Replace("Service", "");
            return puzzleInputFile;
        }

        public string FullInputFile() => SampleInputFile() + "_full";

        public string PuzzleSolutionCode => $"https://github.com/sylvain69780/AdventOfCode2022web/tree/master/AdventOfCode2022/{PuzzleContext!.GetType().Name.Replace("Service", "")}";

        public async Task LoadDefaultPuzzleInput() => await LoadPuzzleInput(SampleInputFile());
        public async Task LoadFullPuzzleInput() => await LoadPuzzleInput(FullInputFile());

        public async Task LoadPuzzleInput(string puzzleInputFile)
            => _input = (await Http!.GetStringAsync($"sample-data/{puzzleInputFile}.txt")).Replace("\r", "");
        public void StartProcessing()
        {
            _stepsToSolution = PuzzleContext!.GetStepsToSolution(_input).GetEnumerator();
            MoveUntilCompleted();
        }

        protected override async Task OnInitializedAsync()
        {
            _stepComputationTimer.Elapsed += (sender, e) => MoveUntilCompleted();
            await LoadDefaultPuzzleInput();
            StartProcessing();
        }

        public void MoveNext()
        {
            if (_stepsToSolution!.MoveNext())
                SolvingStep = _stepsToSolution!.Current.Step;
            else
                PageState = PageState.Finished;
        }

        public ProcessingProgressModel? Result { get; private set; }


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

