using System.Text.Json.Serialization;
using EventPlanner.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.WebAPI.Requests;

public class ReplyToInvitationRequest
{
    public string UserEmail { get; init; }
    public bool Accepted { get; init; }

    public ReplyToInvitationDTO ToDomain(Guid invitationId)
    {
        return new ReplyToInvitationDTO(invitationId, Accepted, UserEmail);
    }
}