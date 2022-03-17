using System;
using EventPlanner.Domain.ValueObjects;

namespace EventPlanner.Domain.Factories;

public static class MessageFactory
{
    public static Message CreateInvitation(string to, string body)
    {
        return new Message(to, "You have been invited for an Occasion", body);
    }

    public static Message CreateRepliedToInvitation(string userEmail, Guid invitationId)
    {
        // TODO: Pull the system email from some kind of configuration
        return new Message("iamchets@yahoo.com", "An invitation has been updated",
            $"{userEmail} has replied to their invitation '{invitationId}'.");
    }
}
