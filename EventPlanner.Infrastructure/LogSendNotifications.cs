using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.ValueObjects;

namespace EventPlanner.Infrastructure;

public class LogSendNotifications : INotify
{
    public Task Notify(Message message)
    {
        Console.WriteLine($"Send an email to: {message.Receiver}");
        Console.WriteLine($"{message.Title}");
        Console.WriteLine($"{message.Body}");

        return Task.CompletedTask;
    }
}