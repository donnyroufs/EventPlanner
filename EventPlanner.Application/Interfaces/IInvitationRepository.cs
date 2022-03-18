using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Application.Interfaces;

public interface IInvitationRepository
{
    public Task<Invitation> Save(Invitation invitation);
    public Task<Invitation> Find(Guid id);
    public Task<List<Invitation>> FindByOccasionId(Guid id);
}