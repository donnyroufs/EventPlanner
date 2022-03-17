using System;

namespace EventPlanner.Domain.Exceptions;

public class CannotCastTheSameVoteException : Exception
{
    public CannotCastTheSameVoteException() : base("You cannot vote for the same thing twice.")
    {
    }
}