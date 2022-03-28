using EventPlanner.Application.UseCases;
using EventPlanner.WebAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.WebAPI.Controllers;

[ApiController]
[Route(("/occasions/{id}/[controller]"))]
public class InvitationsController : ControllerBase
{
    private readonly InviteUserUseCase _inviteUserUseCase;
    private readonly ReplyToInvitationUseCase _replyToInvitationUseCase;

    public InvitationsController(InviteUserUseCase inviteUserUseCase,
        ReplyToInvitationUseCase replyToInvitationUseCase)
    {
        _inviteUserUseCase = inviteUserUseCase;
        _replyToInvitationUseCase = replyToInvitationUseCase;
    }

    [HttpPost]
    public async Task Store([FromBody] InviteUserRequest data, [FromRoute] Guid id)
    {
        await _inviteUserUseCase.Execute(data.ToDomain(id));
    }

    [HttpPost("/occasions/{id}/invitations/{invitationId}/reply")]
    public async Task ReplyToInvitation([FromBody] ReplyToInvitationRequest data,
        [FromRoute(Name = "invitationId")] Guid invitationId)
    {
        await _replyToInvitationUseCase.Execute(data.ToDomain(invitationId));
    }
}