using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Diet
{
    public int Id { get; set; }

    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public int TargetCalories { get; set; }

    public ICollection<DietPlanDay> PlanDays { get; set; } = new List<DietPlanDay>();
}