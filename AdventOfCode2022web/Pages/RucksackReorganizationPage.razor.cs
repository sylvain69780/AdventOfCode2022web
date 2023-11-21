using Domain.RucksackReorganization;

namespace Blazor.Pages
{
    public partial class RucksackReorganizationPage
    {
        RucksackReorganizationService _puzzleService = new(new RucksackReorganizationPart1Strategy());
    }
}
