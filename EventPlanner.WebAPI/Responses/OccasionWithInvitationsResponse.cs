using EventPlanner.Application.DTOs;

namespace EventPlanner.WebAPI.Responses;

public class OccasionWithInvitationsResponse
{
    public OccasionResponse Occasion { get; set; }
    public List<InvitationResponse> Invitations { get; set; }

    public OccasionWithInvitationsResponse(OccasionResponse occasion, List<InvitationResponse> invitations)
    {
        Occasion = occasion;
        Invitations = invitations;
    }

    public static OccasionWithInvitationsResponse From(OccasionWithInvitationsDTO dto)
    {
        return new OccasionWithInvitationsResponse(OccasionResponse.From(dto.Occasion),
            dto.Invitations.Select(InvitationResponse.FromDomain).ToList());
    }
}