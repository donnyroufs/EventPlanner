using System.Threading.Tasks;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Application.Interfaces;

public interface INotify
{
    public Task Notify(Message message);
}