using Domain.CampCleanup;

namespace Blazor.Pages
{
    public partial class CampCleanupPage
    {
        CampCleanupService _puzzleService = new(new CampCleanupPart1Strategy());
    }
}
