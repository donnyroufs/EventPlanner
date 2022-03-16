using System;
using EventPlanner.Domain.Enums;
using EventPlanner.Shared;

namespace EventPlanner.Domain.Entities;

public class Invitation : Entity
{
    public Guid OccasionId { get; init; }
    public string UserEmail { get; init; }
    public InvitationStatus Status { get; set; }

    public Invitation(Guid occasionId, InvitationStatus status, string userEmail)
    {
        OccasionId = occasionId;
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