using EventPlanner.Application.DTOs;

namespace EventPlanner.WebAPI.Requests;

public class ReplyToInvitationRequest
{
    public Guid InvitationId { get; init; }
    public string UserEmail { get; init; }
    public bool Accepted { get; init; }

    public ReplyToInvitationDTO ToDomain()
    {
        return new ReplyToInvitationDTO(InvitationId, Accepted, UserEmail);
    }
}