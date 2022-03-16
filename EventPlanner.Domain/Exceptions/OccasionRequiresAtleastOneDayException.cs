using System;

namespace EventPlanner.Domain.Exceptions;

public class OccasionRequiresAtleastOneDayException : Exception
{
    public OccasionRequiresAtleastOneDayException() : base("An Occasion requires at least 1 day.")
    {
    }
}