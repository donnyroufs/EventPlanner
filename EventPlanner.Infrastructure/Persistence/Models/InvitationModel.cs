using EventPlanner.Domain.Enums;

namespace EventPlanner.Infrastructure.Persistence.Models;

public class InvitationModel
{
    public Guid Id { get; set; }
    public string UserEmail { get; set; }
    public InvitationStatus Status { get; set; }
    public Guid OccasionId { get; set; }
}