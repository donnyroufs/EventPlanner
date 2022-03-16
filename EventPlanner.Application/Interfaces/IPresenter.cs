namespace EventPlanner.Application.Interfaces;

public interface IPresenter<Input, Output>
{
    public Output Present(Input data);
}