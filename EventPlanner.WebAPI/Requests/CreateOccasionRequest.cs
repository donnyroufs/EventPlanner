using System.ComponentModel.DataAnnotations;
using EventPlanner.Application.DTOs;

namespace EventPlanner.WebAPI.Requests;

public class CreateOccasionRequest
{
    [Required] public string Description { get; init; }
    public List<DayOfWeek> Days { get; init; }

    public CreateOccasionDTO ToDomain()
    {
        return new CreateOccasionDTO(Description, Days);
    }
}