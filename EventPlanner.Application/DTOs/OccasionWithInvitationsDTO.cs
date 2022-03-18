using System.Collections.Generic;

namespace EventPlanner.Application.DTOs;

public class OccasionWithInvitationsDTO
{
    public OccasionDTO Occasion { get; set; }
    public List<InvitationDTO> Invitations { get; set; }

    public OccasionWithInvitationsDTO(OccasionDTO occasion, List<InvitationDTO> invitations)
    {
        Occasion = occasion;
        Invitations = invitations;
    }
}