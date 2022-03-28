using Microsoft.EntityFrameworkCore;
using EventPlanner.Domain.Entities;
using EventPlanner.Application.Interfaces;
using EventPlanner.Infrastructure.Persistence.Models;

namespace EventPlanner.Infrastructure.Persistence.Repositories;

public class OccasionRepository : IOccasionRepository
{
    private readonly AppDbContext _context;

    public OccasionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Occasion> Save(Occasion occasion)
    {
        var existingOccasion = await _context.Occasions
            .Include(x => x.Invitations)
            .Include(x => x.Days)
            .FirstOrDefaultAsync(o => o.Id == occasion.Id);

        var model = OccasionMapper.ToModel(occasion);

        if (existingOccasion is null)
        {
            _context.Add(model);
        }

        if (existingOccasion is not null)
        {
            await Update(existingOccasion, model);
        }

        await _context.SaveChangesAsync();

        return occasion;
    }

    public async Task<Occasion?> Find(Guid id)
    {
        var model = await _context.Occasions
            .AsNoTracking()
            .Include(x => x.Days)
            .Include(x => x.Invitations)
            .FirstOrDefaultAsync(occasionModel => occasionModel.Id == id);

        return model is null ? null : OccasionMapper.ToDomain(model);
    }

    public async Task<Occasion?> FindWhereInvitationId(Guid id)
    {
        var invitation = await _context.Invitations
            .Where(invitation => invitation.Id == id)
            .Include(inv => inv.Occasion)
            .ThenInclude(occasion => occasion.Days)
            .FirstOrDefaultAsync();

        return invitation is null ? null : OccasionMapper.ToDomain(invitation.Occasion);
    }


    public async Task<List<Occasion>> FindMany()
    {
        var occasions = await _context.Occasions
            .AsNoTracking()
            .Include(x => x.Days)
            .Include(x => x.Invitations)
            .ToListAsync();

        return occasions.Select(OccasionMapper.ToDomain).ToList();
    }

    private async Task Update(OccasionModel existingOccasion, OccasionModel model)
    {
        _context.Entry(existingOccasion).CurrentValues.SetValues(model);

        foreach (var invitation in model.Invitations)
        {
            var existingInvitation = await _context.Invitations.FirstOrDefaultAsync(inv => inv.Id == invitation.Id);

            if (existingInvitation is null)
            {
                existingOccasion.Invitations.Add(invitation);
            }
            else
            {
                _context.Entry(existingInvitation).CurrentValues.SetValues(invitation);
            }
        }
    }
}