using EventPlanner.Application.UseCases;
using EventPlanner.WebAPI.Requests;
using EventPlanner.WebAPI.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.WebAPI.Controllers;

[ApiController]
[Route(("/occasions/{id}/[controller]"))]
public class InvitationsController : ControllerBase
{
    private readonly InviteUserUseCase<InvitationResponse> _inviteUserUseCase;
    private readonly ReplyToInvitationUseCase<InvitationResponse> _replyToInvitationUseCase;

    public InvitationsController(InviteUserUseCase<InvitationResponse> inviteUserUseCase,
        ReplyToInvitationUseCase<InvitationResponse> replyToInvitationUseCase)
    {
        _inviteUserUseCase = inviteUserUseCase;
        _replyToInvitationUseCase = replyToInvitationUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Store([FromBody] InviteUserRequest data, [FromRoute] Guid id)
    {
        var result = await _inviteUserUseCase.Execute(data.ToDomain(id));

        return Created(nameof(Store), new InvitationResponse(result.Id, result.Status));
    }

    [HttpPost("/occasions/{id}/invitations/{invitationId}/reply")]
    public async Task<IActionResult> ReplyToInvitation([FromBody] ReplyToInvitationRequest data,
        [FromRoute] Guid invitationId)
    {
        var result = await _replyToInvitationUseCase.Execute(data.ToDomain(invitationId));

        return Created(nameof(ReplyToInvitation), result);
    }
}