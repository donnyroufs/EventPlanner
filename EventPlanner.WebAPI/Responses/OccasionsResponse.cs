using EventPlanner.Application.DTOs;

namespace EventPlanner.WebAPI.Responses;

public class OccasionsResponse
{
    public List<OccasionResponse> Occasions { get; init; }

    public OccasionsResponse(List<OccasionResponse> occasions)
    {
        Occasions = occasions;
    }

    public static OccasionsResponse From(List<OccasionDTO> occasions)
    {
        return new OccasionsResponse(occasions.Select(OccasionResponse.From).ToList());
    }
}