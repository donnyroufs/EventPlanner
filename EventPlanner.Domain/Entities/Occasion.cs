using System;
using System.Collections.Generic;
using System.Linq;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Shared;

namespace EventPlanner.Domain.Entities;

public class Occasion : AggregateRoot
{
    public List<DayOfWeek> Days { get; init; }
    public string Description { get; init; }
    public List<Invitation> Invitations { get; private set; } = new();

    public Occasion(string description, List<DayOfWeek> days)
    {
        ThrowWhenInvalidInput(description, days);
        Description = description;
        Days = days;
    }

    public Occasion(Guid id, string description, List<DayOfWeek> days)
    {
        ThrowWhenInvalidInput(description, days);

        Id = id;
        Description = description;
        Days = days;
    }

    public Occasion(Guid id, string description, List<DayOfWeek> days, List<Invitation> invitations)
    {
        ThrowWhenInvalidInput(description, days);

        Id = id;
        Description = description;
        Days = days;
        Invitations = invitations;
    }

    public void AddInvitation(Invitation invitation)
    {
        if (Invitations.Exists(inv => inv.Id == invitation.Id))
        {
            throw new InvitationAlreadyExists();
        }

        Invitations.Add(invitation);
    }

    public void AcceptInvitation(Guid invitationId)
    {
        var invitation = Invitations.FirstOrDefault(inv => inv.Id == invitationId);

        if (invitation is null)
        {
            // TODO: Refactor and test
            throw new Exception("Invitation does not exist");
        }

        invitation.Accept();
    }

    private void ThrowWhenInvalidInput(string description, List<DayOfWeek> days)
    {
        if (!days.Any()) throw new ValidationException("An occasion requires at least one day");
        if (string.IsNullOrEmpty(description)) throw new ValidationException("A description cannot be empty");
    }
}