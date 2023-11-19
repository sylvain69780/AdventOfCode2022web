namespace Blazor
{
    public interface IPuzzleSolutionController
    {
        int AnimationDuration { get; set; }
        string PuzzleSolutionCode { get; }
        string Input { get; set; }
        string Result { get; }
        int SolvingStep { get; }
        PageState PageState { get; }

        string FullInputFile();
        Task LoadDefaultPuzzleInput();
        Task LoadFullPuzzleInput();
        Task LoadPuzzleInput(string puzzleInputFile);
        void MoveNext();
        void MoveUntilCompleted();
        string SampleInputFile();
        void StartPart1();
        void StartPart2();
        void Stop();
    }
}