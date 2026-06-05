using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Diet
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nazwa diety jest wymagana")]
    [MaxLength(150)]
    [Display(Name = "Nazwa diety")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    [Display(Name = "Opis diety")]
    public string? Description { get; set; }

    [Display(Name = "Cel kaloryczny (kcal)")]
    public int TargetCalories { get; set; }
    public ICollection<DietPlanDay> PlanDays { get; set; } = new List<DietPlanDay>();
}