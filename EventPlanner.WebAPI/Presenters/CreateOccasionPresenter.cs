using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.WebAPI.Responses;

namespace EventPlanner.WebAPI.Presenters;

public class CreateOccasionPresenter : ICreateOccasionPresenter<OccasionResponse>
{
    public OccasionResponse Present(OccasionDTO data)
    {
        return OccasionResponse.From(data);
    }
}