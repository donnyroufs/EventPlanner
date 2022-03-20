using System;

namespace EventPlanner.Application.Exceptions;

public class InvitationDoesNotExistException : Exception
{
    public InvitationDoesNotExistException(Guid id) : base($"The invitation: '{id}' does not exist.")
    {
    }
}