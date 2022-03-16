using System;
using System.Collections.Generic;
using System.Linq;

namespace EventPlanner.Domain;

public class Ocassion
{
    public List<DayOfWeek> Days { get; init; }
    public string Description { get; init; }

    public Ocassion(string description, List<DayOfWeek> days)
    {
        if (!days.Any()) throw new OcassionRequiresAtleastOneDayException();

        Description = description;
        Days = days;
    }
}