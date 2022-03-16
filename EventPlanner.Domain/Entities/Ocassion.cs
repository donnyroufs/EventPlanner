using System;
using System.Collections.Generic;
using System.Linq;
using EventPlanner.Domain.Exceptions;

namespace EventPlanner.Domain.Entities;

public class Ocassion
{
    public Guid Id { get; } = Guid.NewGuid();
    public List<DayOfWeek> Days { get; init; }
    public string Description { get; init; }

    public Ocassion(string description, List<DayOfWeek> days)
    {
        if (!days.Any()) throw new OcassionRequiresAtleastOneDayException();

        Description = description;
        Days = days;
    }
}