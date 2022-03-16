using System;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Enums;

namespace EventPlanner.Application.DTOs;

public class InvitationDTO
{
    public Guid Id { get; init; }
    public InvitationStatus Status { get; init; }

    private InvitationDTO(Guid id, InvitationStatus status)
    {
        Id = id;
        Status = status;
    }

    public static InvitationDTO From(Invitation entity)
    {
        return new InvitationDTO(entity.Id, entity.Status);
    }
}