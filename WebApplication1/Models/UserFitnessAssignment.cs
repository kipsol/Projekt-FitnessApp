using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models;

public class UserFitnessAssignment
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public IdentityUser User { get; set; } = null!;

    public int? PlanTreningowyId { get; set; }

    public PlanTreningowy? PlanTreningowy { get; set; }

    public int? DietId { get; set; }

    public Diet? Diet { get; set; }

    public string AssignedByTrainerId { get; set; } = string.Empty;

    public IdentityUser AssignedByTrainer { get; set; } = null!;

    public DateTime AssignedAtUtc { get; set; } = DateTime.UtcNow;
}
