namespace EventPlanner.Infrastructure.Persistence.Models;

public class OccasionDaysModel
{
    public Guid Id { get; set; }
    public DayOfWeek Day { get; set; }

    public OccasionDaysModel(DayOfWeek day)
    {
        Day = day;
    }
}