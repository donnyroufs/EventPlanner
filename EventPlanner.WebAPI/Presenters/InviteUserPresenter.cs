using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Enums;
using EventPlanner.WebAPI.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.WebAPI.Presenters;

public class InviteUserPresenter : IInviteUserPresenter
{
    private readonly IHttpContextAccessor _context;

    public InviteUserPresenter(IHttpContextAccessor context)
    {
        _context = context;
    }

    public async Task Present(OccasionWithInvitationDTO data)
    {
        // TODO: Check if nameof makes sense here, probably not.
        var created = new CreatedResult(nameof(Present), InvitationResponse.FromDomain(data.Invitation));

        _context.HttpContext!.Response.StatusCode = created.StatusCode!.Value;
        await _context.HttpContext!.Response.WriteAsJsonAsync(created.Value);
    }
}