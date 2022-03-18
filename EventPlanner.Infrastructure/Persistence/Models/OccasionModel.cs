using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Infrastructure.Persistence.Models;

public class OccasionModel
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public ICollection<OccasionDaysModel> Days { get; set; }
}