using System;
using System.Collections.Generic;

namespace EventPlanner.Application.DTOs;

public class CreateOccasionDTO
{
    public string Description { get; init; }
    public List<DayOfWeek> Days { get; init; }

    public CreateOccasionDTO(string description, List<DayOfWeek> days)
    {
        Description = description;
        Days = days;
    }
}