using System;
using System.Collections.Generic;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Application.DTOs;

public class OcassionDTO
{
    public string Description { get; init; }
    public List<DayOfWeek> Days { get; init; }

    private OcassionDTO(string description, List<DayOfWeek> days)
    {
        Description = description;
        Days = days;
    }

    public static OcassionDTO From(Ocassion entity)
    {
        return new OcassionDTO(entity.Description, entity.Days);
    }
}