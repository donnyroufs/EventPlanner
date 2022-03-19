using System.Threading.Tasks;

namespace EventPlanner.Shared;

public interface IUseCase<Input, Output>
{
    public Task<Output> Execute(Input data);
}

public interface IUseCase<Output>
{
    public Task<Output> Execute();
}