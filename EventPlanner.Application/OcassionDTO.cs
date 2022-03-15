namespace EventPlanner.Application
{
    public class OcassionDTO
    {
        public string Description { get; }

        public OcassionDTO(string description)
        {
            Description = description;
        }
    }
}