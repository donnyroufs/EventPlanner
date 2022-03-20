using EventPlanner.Domain.Entities;
using EventPlanner.Application.Interfaces;
using EventPlanner.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Infrastructure.Persistence.Repositories;

public class InvitationRepository : IInvitationRepository
{
    private readonly AppDbContext _context;

    public InvitationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Invitation> Save(Invitation invitation)
    {
        await _context.Invitations.AddAsync(new InvitationModel
        {
            Id = invitation.Id,
            Status = invitation.Status,
            OccasionId = invitation.OccasionId,
            UserEmail = invitation.UserEmail
        });

        await _context.SaveChangesAsync();

        return invitation;
    }

    public async Task<Invitation?> Find(Guid id)
    {
        var foundInvitation = await _context.Invitations.FirstOrDefaultAsync(inv => inv.Id == id);

        if (foundInvitation is null)
        {
            return null;
        }

        return new Invitation(foundInvitation.Id, foundInvitation.Status, foundInvitation.UserEmail);
    }

    public async Task<List<Invitation>> FindByOccasionId(Guid id)
    {
        var invitations = await _context.Invitations.Where(x => x.OccasionId == id).ToListAsync();

        return invitations.Select(x => new Invitation(x.Id, x.OccasionId, x.Status, x.UserEmail)).ToList();
    }
}