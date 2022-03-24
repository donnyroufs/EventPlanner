using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Infrastructure.Persistence.Models;

public class OccasionModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    public string Description { get; set; }
    public ICollection<OccasionDaysModel> Days { get; set; }
    public ICollection<InvitationModel> Invitations { get; set; }
}