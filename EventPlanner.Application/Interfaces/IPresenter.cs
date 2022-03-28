using System.Threading.Tasks;

namespace EventPlanner.Application.Interfaces;

public interface IPresenter<Input>
{
    public Task Present(Input data);
}