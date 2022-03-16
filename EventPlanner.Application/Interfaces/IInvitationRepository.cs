using System;
using System.Threading.Tasks;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Application.Interfaces;

public interface IInvitationRepository
{
    public Task<Invitation> Save(Invitation invitation);
    public Task<Invitation> Find(Guid id);
}