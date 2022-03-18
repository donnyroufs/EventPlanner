using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.WebAPI.Responses;

namespace EventPlanner.WebAPI.Presenters;

public class GetOccasionPresenter : IGetOccasionPresenter<OccasionWithInvitationsResponse>
{
    public OccasionWithInvitationsResponse Present(OccasionWithInvitationsDTO data)
    {
        return OccasionWithInvitationsResponse.From(data);
    }
}