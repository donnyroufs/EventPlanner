using System;

namespace EventPlanner.Application.DTOs;

public interface IInviteUserDTO
{
    public Guid OccasionId { get; init; }
    public string Receiver { get; init; }
}