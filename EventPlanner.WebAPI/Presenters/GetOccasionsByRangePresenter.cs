using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.WebAPI.Responses;

namespace EventPlanner.WebAPI.Presenters;

public class GetOccasionsByRangePresenter : IGetOccasionsByRangePresenter<OccasionsResponse>
{
    public OccasionsResponse Present(List<OccasionDTO> data)
    {
        return OccasionsResponse.From(data);
    }
}