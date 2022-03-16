using System;

namespace EventPlanner.Application.Exceptions;

public class OccasionDoesNotExistException : Exception
{
    public OccasionDoesNotExistException() : base("The Occasion you tried to access does not exist")
    {
    }
}