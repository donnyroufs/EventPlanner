using EventPlanner.Application.DTOs;
using EventPlanner.Domain.Enums;

namespace EventPlanner.WebAPI.Responses;

public class InvitationResponse
{
    public Guid Id { get; init; }
    public string Status { get; init; }

    public InvitationResponse(Guid id, string status)
    {
        Id = id;
        Status = status;
    }

    public static InvitationResponse FromDomain(InvitationDTO data)
    {
        return new InvitationResponse(data.Id, MapEnumToStringValue(data.Status));
    }

    private static string MapEnumToStringValue(InvitationStatus status)
    {
        return Enum.GetName(typeof(InvitationStatus), status)!;
    }
}