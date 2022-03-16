namespace EventPlanner.Domain.Entities;

public class Message
{
    public string Title { get; init; }
    public string Body { get; init; }
    public string Receiver { get; init; }

    public Message(string receiver, string title, string body)
    {
        Receiver = receiver;
        Title = title;
        Body = body;
    }
}