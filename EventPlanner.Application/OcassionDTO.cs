using EventPlanner.Domain;

namespace EventPlanner.Application;

public class OcassionDTO
{
    public string Description { get; }

    protected OcassionDTO(string description)
    {
        Description = description;
    }

    public static OcassionDTO Create(Ocassion entity)
    {
        return new OcassionDTO(entity.Description);
    }
}