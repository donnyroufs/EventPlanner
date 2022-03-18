using EventPlanner.Application.DTOs;

namespace EventPlanner.WebAPI.Requests;

public class InviteUserRequest
{
    public Guid OccasionId { get; init; }
    public string Receiver { get; init; }

    public InviteUserDTO ToDomain()
    {
        return new InviteUserDTO(OccasionId, Receiver);
    }
}