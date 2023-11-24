using Domain.CalorieCounting;
using Domain;

namespace Domain
{
    public interface IPuzzleStrategy<TModel>
    {
        string Name { get; set; }
        IEnumerable<ProcessingProgressModel> GetSteps(TModel model,Func<ProcessingProgressModel> updateContext, Action<string> provideSolution);
    }
}
