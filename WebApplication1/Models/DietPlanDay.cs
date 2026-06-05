using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class DietPlanDay
{
    public int Id { get; set; }
    public int DietId { get; set; }
    public Diet? Diet { get; set; }
    public int MealId { get; set; }
    public Meal? Meal { get; set; }

    [Required]
    [Display(Name = "Dzień tygodnia")]
    public DayOfWeek Day { get; set; }

    [Required]
    [MaxLength(50)]
    [Display(Name = "Posiłek")]
    public string MealType { get; set; } = "Śniadanie";
}