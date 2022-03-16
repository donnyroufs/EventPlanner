using EventPlanner.Domain.Entities;
using EventPlanner.Domain.ValueObjects;

namespace EventPlanner.Domain.Factories;

public static class MessageFactory
{
    public static Message CreateInvitation(string to, string body)
    {
        return new Message(to, "You have been invited for an Occasion", body);
    }
}