using System;
using EventPlanner.Domain.Enums;

namespace EventPlanner.Domain.Entities;

public class Invitation
{
    public Guid Id { get; init; }
    public Guid OcassionId { get; init; }
    public string UserEmail { get; init; }
    public InvitationStatus Status { get; set; }

    public Invitation(Guid id, Guid ocassionId, InvitationStatus status, string userEmail)
    {
        Id = id;
        OcassionId = ocassionId;
        Status = status;
        UserEmail = userEmail;
    }

    public void Accept()
    {
        Status = InvitationStatus.Accepted;
    }

    public void Decline()
    {
        Status = InvitationStatus.Declined;
    }
}