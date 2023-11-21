using Domain.RucksackReorganization;

namespace Blazor.Pages
{
    public partial class RucksackReorganizationPage2
    {
        RucksackReorganizationService _puzzleService = new(new RucksackReorganizationPart1Strategy());
    }
}
