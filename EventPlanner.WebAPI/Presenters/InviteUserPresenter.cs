using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Enums;
using EventPlanner.WebAPI.Responses;

namespace EventPlanner.WebAPI.Presenters;

public class InviteUserPresenter : IInviteUserPresenter<InvitationResponse>
{
    public InvitationResponse Present(OccasionWithInvitationDTO data)
    {
        return InvitationResponse.FromDomain(data.Invitation);
    }
}