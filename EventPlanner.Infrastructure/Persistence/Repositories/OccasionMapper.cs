using EventPlanner.Domain.Entities;
using EventPlanner.Infrastructure.Persistence.Models;

namespace EventPlanner.Infrastructure.Persistence.Repositories;

public static class OccasionMapper
{
    public static Occasion ToDomain(OccasionModel model)
    {
        var days = model.Days.Select(day => day.Day).ToList();
        var invitations = model.Invitations
            .Select(inv => new Invitation(inv.Id, inv.OccasionId, inv.Status, inv.UserEmail)).ToList();

        return new Occasion(model.Id, model.Description, days, invitations);
    }

    public static OccasionModel ToModel(Occasion occasion)
    {
        return new OccasionModel
        {
            Id = occasion.Id,
            Days = occasion.Days.Select(x => new OccasionDaysModel(x)).ToList(),
            Description = occasion.Description,
            Invitations = occasion.Invitations.Select(inv => new InvitationModel
            {
                Id = inv.Id,
                Status = inv.Status,
                OccasionId = inv.OccasionId,
                UserEmail = inv.UserEmail
            }).ToList(),
        };
    }
}