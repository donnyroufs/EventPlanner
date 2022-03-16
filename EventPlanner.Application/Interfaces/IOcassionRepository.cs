using System.Threading.Tasks;
using EventPlanner.Domain;

namespace EventPlanner.Application.Interfaces;

public interface IOcassionRepository
{
    Task<Ocassion> Save(Ocassion ocassion);
}