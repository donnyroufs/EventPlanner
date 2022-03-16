using System.Threading.Tasks;
using EventPlanner.Domain.ValueObjects;

namespace EventPlanner.Application.Interfaces;

public interface INotify
{
    public Task Notify(Message message);
}