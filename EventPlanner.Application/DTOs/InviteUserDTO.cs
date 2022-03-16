using System;

namespace EventPlanner.Application.DTOs;

public class InviteUserDTO : IInviteUserDTO
{
    public Guid OcassionId { get; init; }
    public string Receiver { get; init; }

    public InviteUserDTO(Guid ocassionId, string receiver)
    {
        OcassionId = ocassionId;
        Receiver = receiver;
    }
}