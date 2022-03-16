using System;
using System.Collections.Generic;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Application.DTOs;

public class OccasionDTO
{
    public string Description { get; init; }
    public List<DayOfWeek> Days { get; init; }

    private OccasionDTO(string description, List<DayOfWeek> days)
    {
        Description = description;
        Days = days;
    }

    public static OccasionDTO From(Occasion entity)
    {
        return new OccasionDTO(entity.Description, entity.Days);
    }
}