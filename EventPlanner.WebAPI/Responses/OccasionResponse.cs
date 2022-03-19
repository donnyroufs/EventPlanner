using System.Text.Json.Serialization;
using EventPlanner.Application.DTOs;

namespace EventPlanner.WebAPI.Responses;

public class OccasionResponse
{
    public Guid Id { get; init; }
    public string Description { get; init; }
    public List<string> Days { get; init; }

    [JsonConstructor]
    public OccasionResponse()
    {
    }

    private OccasionResponse(Guid id, string description, List<string> days)
    {
        Id = id;
        Description = description;
        Days = days;
    }

    public static OccasionResponse From(OccasionDTO dto)
    {
        var days = dto.Days.Select(day => MapEnumToStringValue(day)).ToList();
        return new OccasionResponse(dto.Id, dto.Description, days);
    }

    private static string MapEnumToStringValue(DayOfWeek day)
    {
        return Enum.GetName(typeof(DayOfWeek), day)!;
    }
}