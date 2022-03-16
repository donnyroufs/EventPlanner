using System;

namespace EventPlanner.Application.DTOs;

public class ReplyToInvitationDTO
{
    public Guid InvitationId { get; init; }
    public string UserEmail { get; init; }
    public bool Accepted { get; init; }

    public ReplyToInvitationDTO(Guid invitationId, bool accepted, string userEmail)
    {
        InvitationId = invitationId;
        Accepted = accepted;
        UserEmail = userEmail;
    }
}