using System;
using System.Collections.Generic;
using System.Linq;
using EventPlanner.Domain.Exceptions;

namespace EventPlanner.Domain.Entities;

public class Occasion
{
    public Guid Id { get; } = Guid.NewGuid();
    public List<DayOfWeek> Days { get; init; }
    public string Description { get; init; }

    public Occasion(string description, List<DayOfWeek> days)
    {
        if (!days.Any()) throw new OccasionRequiresAtleastOneDayException();

        Description = description;
        Days = days;
    }
}