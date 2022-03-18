using Microsoft.EntityFrameworkCore;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.ValueObjects;
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
        var days = occasion.Days.Select(x => new OccasionDaysModel(x)).ToList();

        var model = new OccasionModel
        {
            Id = occasion.Id,
            Days = days,
            Description = occasion.Description
        };

        _context.Add(model);

        await _context.SaveChangesAsync();

        return occasion;
    }

    public async Task<Occasion> Find(Guid id)
    {
        var model = await _context.Occasions.Include(x => x.Days).FirstAsync(occasionModel => occasionModel.Id == id);

        if (model == null) return null;

        return new Occasion(model.Id, model.Description, model.Days.Select(x => x.Day).ToList());
    }

    public async Task<List<Occasion>> FindByRange(DateRange range)
    {
        var occasions = await _context.Occasions.Include(x => x.Days).ToListAsync();

        return occasions.Select(x => new Occasion(x.Id, x.Description, x.Days.Select(i => i.Day).ToList())).ToList();
    }
}