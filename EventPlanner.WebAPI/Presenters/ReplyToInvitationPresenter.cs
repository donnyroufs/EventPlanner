using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.WebAPI.Responses;

namespace EventPlanner.WebAPI.Presenters;

public class ReplyToInvitationPresenter : IReplyToInvitationPresenter<InvitationResponse>
{
    public InvitationResponse Present(InvitationDTO data)
    {
        return InvitationResponse.FromDomain(data);
    }
}