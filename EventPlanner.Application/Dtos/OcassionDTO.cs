using System;
using System.Collections.Generic;
using EventPlanner.Domain;

namespace EventPlanner.Application.Dtos;

public class OcassionDTO
{
    public string Description { get; init; }
    public List<DayOfWeek> Days { get; init; }

    protected OcassionDTO(string description, List<DayOfWeek> days)
    {
        Description = description;
        Days = days;
    }

    public static OcassionDTO Create(Ocassion entity)
    {
        return new OcassionDTO(entity.Description, entity.Days);
    }
}