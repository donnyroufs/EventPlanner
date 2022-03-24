using System;

namespace EventPlanner.Domain.Exceptions;

public class InvitationAlreadyExists : Exception
{
    public InvitationAlreadyExists() : base("The current invitation already exists for the occasion")
    {
    }
}