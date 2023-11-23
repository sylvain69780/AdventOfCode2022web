using Domain.Sudoku;

namespace Blazor.Pages
{
    public partial class SudokuPage
    {
        SudokuService _puzzleService = new(new SudokuStrategy());
    }
}
