﻿using AdventOfCode2022web.Puzzles;

namespace AdventOfCode2022web
{

    public interface IPuzzleSolutionViewModel
    {
        IPuzzleSolutionIter PuzzleSolution { get; }
        int AnimationDuration { get; set; }
        int SolvingStep { get; set; }
        Action? PuzzleOutputReturned { get; set; }
        void NotifyPuzzleOutputReturned();
        Action? PuzzleInputNeeded { get; set; }
        void NotifyPuzzleInputNeeded();
        Action? PuzzleInputLoaded { get; set; }
        void NotifyPuzzleInputLoaded();
    }

    public class PuzzleSolutionViewModel : IPuzzleSolutionViewModel
    {
        private IPuzzleSolutionIter _puzzleSolution;
        public IPuzzleSolutionIter PuzzleSolution => _puzzleSolution;

        public PuzzleSolutionViewModel(IPuzzleSolutionIter solution)
        {
            _puzzleSolution = solution;
        }

        public int AnimationDuration { get; set; } = 500;
        public int SolvingStep { get; set; } = 0;

        public Action? PuzzleOutputReturned { get; set; }
        public void NotifyPuzzleOutputReturned() => PuzzleOutputReturned?.Invoke();

        public Action? PuzzleInputNeeded { get; set; }
        public void NotifyPuzzleInputNeeded() => PuzzleInputNeeded?.Invoke();

        public Action? PuzzleInputLoaded { get; set; }
        public void NotifyPuzzleInputLoaded() => PuzzleInputLoaded?.Invoke();
    }

    public class PuzzleSolutionViewModel<T> : IPuzzleSolutionViewModel where T : IPuzzleSolutionIter
    {
        private T _puzzleSolution;
        public IPuzzleSolutionIter PuzzleSolution { get => _puzzleSolution; }

        public PuzzleSolutionViewModel(T solution)
        {
            _puzzleSolution = solution;
        }

        public int AnimationDuration { get; set; } = 500;
        public int SolvingStep { get; set; } = 0;

        public Action? PuzzleOutputReturned { get; set; }
        public void NotifyPuzzleOutputReturned() => PuzzleOutputReturned?.Invoke();

        public Action? PuzzleInputNeeded { get; set; }
        public void NotifyPuzzleInputNeeded() => PuzzleInputNeeded?.Invoke();

        public Action? PuzzleInputLoaded { get; set; }
        public void NotifyPuzzleInputLoaded() => PuzzleInputLoaded?.Invoke();
    }

    public class PuzzleBasicSolutionViewModel<T> : IPuzzleSolutionViewModel where T : IPuzzleSolution
    {
        private PuzzleSolutionWrapper _puzzleSolution;
        public IPuzzleSolutionIter PuzzleSolution { get => _puzzleSolution; }

        public PuzzleBasicSolutionViewModel(T solution)
        {
            _puzzleSolution = new PuzzleSolutionWrapper(solution);
        }

        public int AnimationDuration { get; set; } = 500;
        public int SolvingStep { get; set; } = 0;

        public Action? PuzzleOutputReturned { get; set; }
        public void NotifyPuzzleOutputReturned() => PuzzleOutputReturned?.Invoke();

        public Action? PuzzleInputNeeded { get; set; }
        public void NotifyPuzzleInputNeeded() => PuzzleInputNeeded?.Invoke();

        public Action? PuzzleInputLoaded { get; set; }
        public void NotifyPuzzleInputLoaded() => PuzzleInputLoaded?.Invoke();
    }
}