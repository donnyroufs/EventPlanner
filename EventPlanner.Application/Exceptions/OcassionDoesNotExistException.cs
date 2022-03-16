using System;

namespace EventPlanner.Application.Exceptions;

public class OcassionDoesNotExistException : Exception
{
    public OcassionDoesNotExistException() : base("The ocassion you tried to access does not exist")
    {
    }
}