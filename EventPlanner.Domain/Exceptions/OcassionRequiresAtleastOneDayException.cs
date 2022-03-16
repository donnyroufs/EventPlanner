using System;

namespace EventPlanner.Domain.Exceptions;

public class OcassionRequiresAtleastOneDayException : Exception
{
    public OcassionRequiresAtleastOneDayException() : base("An ocassion requires at least 1 day.")
    {
    }
}