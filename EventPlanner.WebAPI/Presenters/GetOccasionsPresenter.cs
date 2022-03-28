using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.WebAPI.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.WebAPI.Presenters;

public class GetOccasionsPresenter : IGetOccasionsPresenter
{
    private readonly IHttpContextAccessor _context;

    public GetOccasionsPresenter(IHttpContextAccessor context)
    {
        _context = context;
    }

    public async Task Present(List<OccasionDTO> data)
    {
        var ok = new OkObjectResult(OccasionsResponse.From(data));

        await _context.HttpContext!.Response.WriteAsJsonAsync(ok.Value);
    }
}