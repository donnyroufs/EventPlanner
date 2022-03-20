using EventPlanner.Application.DTOs;

namespace EventPlanner.WebAPI.Requests;

public class InviteUserRequest
{
    public string Receiver { get; init; }

    public InviteUserDTO ToDomain(Guid occasionId)
    {
        return new InviteUserDTO(occasionId, Receiver);
    }
}