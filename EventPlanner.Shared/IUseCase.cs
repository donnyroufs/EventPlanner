using System.Threading.Tasks;

namespace EventPlanner.Shared;

public interface IUseCase<Input, Output>
{
    public Task<Output> Execute(Input data);
}