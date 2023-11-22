using Domain.CampCleanup;

namespace Blazor.Pages
{
    public partial class CampCleanupPage2
    {
        CampCleanupService _puzzleService = new(new CampCleanupPart2Strategy());
    }
}
