using System;
using System.Threading.Tasks;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Application.Interfaces;

public interface IOcassionRepository
{
    Task<Ocassion> Save(Ocassion ocassion);
    Task<Ocassion> Find(Guid id);
}