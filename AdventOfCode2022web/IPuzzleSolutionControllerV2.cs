using AdventOfCode2022Solutions.PuzzleSolutions;

namespace AdventOfCode2022web
{
    public interface IPuzzleSolutionControllerV2
    {
        string PuzzleSolutionCode { get; }
        string Input { get; set; }
        IPuzzleSolverDTO? Result { get; }
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
