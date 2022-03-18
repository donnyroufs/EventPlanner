using EventPlanner.Application.Interfaces;
using EventPlanner.Infrastructure.Persistence;
using EventPlanner.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlanner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<AppDbContext>();
        services.AddScoped<IOccasionRepository, OccasionRepository>();
        services.AddScoped<IInvitationRepository, InvitationRepository>();
        services.AddScoped<INotify, LogSendNotifications>();

        return services;
    }
}