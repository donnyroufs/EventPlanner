using System;

namespace EventPlanner.Application.DTOs;

public interface IInviteUserDTO
{
    public Guid OcassionId { get; init; }
    public string Receiver { get; init; }
}