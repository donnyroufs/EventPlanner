namespace EventPlanner.Infrastructure.Persistence.Models;

public class OccasionDaysModel
{
    public Guid Id { get; set; }
    public DayOfWeek Day { get; set; }
    public OccasionModel Occasion;

    public OccasionDaysModel(DayOfWeek day)
    {
        Day = day;
    }
}