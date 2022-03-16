using System;

namespace EventPlanner.Domain;

public class OcassionRequiresAtleastOneDayException : Exception
{
    public OcassionRequiresAtleastOneDayException() : base("An ocassion requires at least 1 day.")
    {
    }
}