using System;
using System.Collections.Generic;
using System.Linq;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Shared;

namespace EventPlanner.Domain.Entities;

public class Occasion : Entity
{
    public List<DayOfWeek> Days { get; init; }
    public string Description { get; init; }

    public Occasion(string description, List<DayOfWeek> days)
    {
        ThrowWhenInvalidInput(description, days);
        Description = description;
        Days = days;
    }

    public Occasion(Guid id, string description, List<DayOfWeek> days)
    {
        ThrowWhenInvalidInput(description, days);

        Id = id;
        Description = description;
        Days = days;
    }

    private void ThrowWhenInvalidInput(string description, List<DayOfWeek> days)
    {
        if (!days.Any()) throw new ValidationException("An occasion requires at least one day");
        if (string.IsNullOrEmpty(description)) throw new ValidationException("A description cannot be empty");
    }
}