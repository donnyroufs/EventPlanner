using System;
using System.Collections.Generic;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Application.DTOs;

public class OccasionDTO
{
    public Guid Id { get; init; }
    public string Description { get; init; }
    public List<DayOfWeek> Days { get; init; }

    private OccasionDTO(Guid id, string description, List<DayOfWeek> days)
    {
        Id = id;
        Description = description;
        Days = days;
    }

    public static OccasionDTO From(Occasion entity)
    {
        return new OccasionDTO(entity.Id, entity.Description, entity.Days);
    }
}