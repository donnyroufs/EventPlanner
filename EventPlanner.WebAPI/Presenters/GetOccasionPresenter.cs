using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.WebAPI.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.WebAPI.Presenters;

public class GetOccasionPresenter : IGetOccasionPresenter
{
    private readonly IHttpContextAccessor _context;

    public GetOccasionPresenter(IHttpContextAccessor context)
    {
        _context = context;
    }

    public async Task Present(OccasionWithInvitationsDTO data)
    {
        var ok = new OkObjectResult(OccasionWithInvitationsResponse.From(data));

        await _context.HttpContext!.Response.WriteAsJsonAsync(ok.Value);
    }
}
