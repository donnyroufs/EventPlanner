using System;

namespace EventPlanner.Application.DTOs;

public class InviteUserDTO : IInviteUserDTO
{
    public Guid OccasionId { get; init; }
    public string Receiver { get; init; }

    public InviteUserDTO(Guid OccasionId, string receiver)
    {
        OccasionId = OccasionId;
        Receiver = receiver;
    }
}