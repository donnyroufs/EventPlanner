namespace EventPlanner.Application;

public interface IPresenter<Input, Output>
{
    public Output Present(Input data);
}