using System.Threading.Tasks;

namespace EventPlanner.Shared;

public interface IUseCase<Input>
{
    public Task Execute(Input data);
}

public interface IUseCase
{
    public Task Execute();
}