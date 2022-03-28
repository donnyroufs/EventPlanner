using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.WebAPI.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.WebAPI.Presenters;

public class ReplyToInvitationPresenter : IReplyToInvitationPresenter
{
    private readonly IHttpContextAccessor _context;

    public ReplyToInvitationPresenter(IHttpContextAccessor context)
    {
        _context = context;
    }

    public async Task Present(InvitationDTO data)
    {
        // TODO: Check if nameof makes sense here, probably not.
        var created = new CreatedResult(nameof(Present), InvitationResponse.FromDomain(data));

        _context.HttpContext!.Response.StatusCode = created.StatusCode!.Value;
        await _context.HttpContext!.Response.WriteAsJsonAsync(created.Value);
    }
}