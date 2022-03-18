using EventPlanner.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<OccasionModel> Occasions { get; set; }
    public DbSet<InvitationModel> Invitations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("event-planner-dev");
    }
}