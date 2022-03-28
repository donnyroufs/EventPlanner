using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.WebAPI.Responses;

namespace EventPlanner.WebAPI.Presenters;

public class GetOccasionPresenter : IGetOccasionPresenter<OccasionWithInvitationsResponse>
{
    // private readonly IHttpContextAccessor _context;
    //
    // public GetOccasionPresenter(IHttpContextAccessor context)
    // {
    //     _context = context;
    // }

    public OccasionWithInvitationsResponse Present(OccasionWithInvitationsDTO data)
    {
        // var ok = new OkObjectResult(OccasionWithInvitationsResponse.From(data));

        // _context.HttpContext.Response.WriteAsJsonAsync(ok.Value);
        // return null;

        return OccasionWithInvitationsResponse.From(data);
    }
}
