using System;

namespace EventPlanner.Application.DTOs;

public class InviteUserDTO
{
    public Guid OccasionId { get; init; }
    public string Receiver { get; init; }

    public InviteUserDTO(Guid occasionId, string receiver)
    {
        OccasionId = occasionId;
        Receiver = receiver;
    }
}