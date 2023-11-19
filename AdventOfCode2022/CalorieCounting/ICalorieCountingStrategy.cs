using Domain.CalorieCounting;

namespace Domain.CalorieCounting
{
    public interface ICalorieCountingStrategy
    {
        public IEnumerable<ProcessingProgressModel> GetSteps(CalorieCountingModel model, Func<ProcessingProgressModel> progressInfo, Action<string> provideSolution);
    }
}
