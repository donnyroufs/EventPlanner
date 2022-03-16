using System;
using System.Threading.Tasks;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Application.Interfaces;

public interface IOccasionRepository
{
    Task<Occasion> Save(Occasion Occasion);
    Task<Occasion> Find(Guid id);
}