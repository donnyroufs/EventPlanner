using EventPlanner.Domain.Entities;

namespace EventPlanner.Application.DTOs;

public class OccasionWithInvitationDTO
{
    public InvitationDTO Invitation { get; set; }
    public OccasionDTO Occasion { get; set; }

    protected OccasionWithInvitationDTO(InvitationDTO invitation, OccasionDTO occasion)
    {
        Invitation = invitation;
        Occasion = occasion;
    }

    public static OccasionWithInvitationDTO From(InvitationDTO invitation, OccasionDTO occasion)
    {
        return new OccasionWithInvitationDTO(invitation, occasion);
    }
}