using System.Threading.Tasks;
using EventPlanner.Domain;

namespace EventPlanner.Application;

public interface IOcassionRepository
{
    Task<Ocassion> Save(Ocassion ocassion);
}