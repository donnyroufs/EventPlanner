using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.ValueObjects;

namespace EventPlanner.Application.Interfaces;

public interface IOccasionRepository
{
    Task<Occasion> Save(Occasion occasion);
    Task<Occasion?> Find(Guid id);
    Task<Occasion?> FindWhereInvitationId(Guid id);
    Task<List<Occasion>> FindMany();
}