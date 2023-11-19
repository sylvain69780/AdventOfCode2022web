using Domain.CalorieCounting;
using Domain;

namespace Domain
{
    public interface IPuzzleStrategy<TModel>
    {
        IEnumerable<ProcessingProgressModel> GetSteps(TModel model,Func<ProcessingProgressModel> updateContext, Action<string> provideSolution);
    }
}
